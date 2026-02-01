using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class WeldingScript : Interactable
{
    public GameObject[] completeActors;
    public Material completedMaterial;

    [Header("Welding Settings")]
    public SplineContainer weldSpline;        // The spline to follow for welding
    public int knotsToGenerate = 10;          // Number of knots to generate along the spline
    public float knotCompletionRadius = 0.1f; // Radius around each knot to mark it complete
    public LayerMask weldPanelLayer;          // Layer for the welding panel
    public Camera playerCamera;               // Reference to player camera for raycasting
    
    [Header("Weld Visual Settings")]
    public GameObject weldMarkPrefab;         // Prefab for weld marks (sphere/cylinder)
    public float weldMarkSize = 0.05f;        // Size of each weld mark
    public Material weldMaterial;             // Material for the weld marks
    public float minPointDistance = 0.05f;    // Minimum distance between weld marks
    public ParticleSystem weldParticles;      // Particle system to play while welding
    
    private PlayerInputActions playerControls;
    private bool isWelding = false;
    private bool[] knotsCompleted;            // Track which knots are completed
    private List<Vector3> weldPoints = new List<Vector3>(); // Points where player has welded
    private Vector3[] splineKnotPositions;    // World positions of knots generated from spline
    private GameObject weldMarksContainer;    // Parent object to hold all weld marks
    
    new void Start()
    {
        base.Start();
        
        Debug.Log("WeldingScript Start() called");
        
        // Generate knot positions from spline
        if (weldSpline != null && knotsToGenerate > 0)
        {
            GenerateKnotsFromSpline();
            knotsCompleted = new bool[splineKnotPositions.Length];
            Debug.Log($"Generated {splineKnotPositions.Length} knots from spline");
        }
        else
        {
            Debug.LogWarning($"Spline setup issue - weldSpline: {weldSpline}, knotsToGenerate: {knotsToGenerate}");
        }
        
        // Setup weld marks container
        weldMarksContainer = new GameObject("WeldMarks");
        weldMarksContainer.transform.SetParent(transform);
        weldMarksContainer.transform.localPosition = Vector3.zero;
        
        // Create default weld mark prefab if not assigned
        if (weldMarkPrefab == null)
        {
            weldMarkPrefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            weldMarkPrefab.transform.localScale = Vector3.one * weldMarkSize;
            if (weldMaterial != null)
            {
                weldMarkPrefab.GetComponent<Renderer>().material = weldMaterial;
            }
            weldMarkPrefab.SetActive(false);
        }
        
        // Setup player controls
        playerControls = new PlayerInputActions();
        playerControls.PlayerController.Enable();
        
        // Auto-find camera if not assigned
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }
    
    void GenerateKnotsFromSpline()
    {
        if (weldSpline == null || weldSpline.Spline == null) return;
        
        splineKnotPositions = new Vector3[knotsToGenerate];
        
        for (int i = 0; i < knotsToGenerate; i++)
        {
            float t = i / (float)(knotsToGenerate - 1); // Normalized position along spline (0 to 1)
            Vector3 localPos = weldSpline.Spline.EvaluatePosition(t);
            splineKnotPositions[i] = weldSpline.transform.TransformPoint(localPos);
        }
    }
    
    void OnDestroy()
    {
        if (playerControls != null)
        {
            playerControls.PlayerController.Disable();
        }
    }
    
    // Called by PlayerInteraction system when interact button is pressed
    new public void Interact()
    {
        isWelding = !isWelding; // Toggle welding on/off
        
        // Control particle system
        if (weldParticles != null)
        {
            if (isWelding)
            {
                weldParticles.Play();
            }
            else
            {
                weldParticles.Stop();
            }
        }
        
        Debug.Log(isWelding ? "Welding started!" : "Welding stopped!");
        base.Interact();
    }
    
    void Update()
    {
        // Check if interact button is being held down
        if (isWelding && playerControls.PlayerController.Interact.IsPressed())
        {
            PerformWelding();
        }
        else if (isWelding && !playerControls.PlayerController.Interact.IsPressed())
        {
            // Stop welding if button released
            isWelding = false;
            
            // Stop particles
            if (weldParticles != null)
            {
                weldParticles.Stop();
            }
            
            Debug.Log("Welding stopped!");
        }
    }
    
    void PerformWelding()
    {
        if (playerCamera == null)
        {
            Debug.LogWarning("Player camera is null!");
            return;
        }
        
        // Raycast from camera center to detect hit on weld panel
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 10f, weldPanelLayer))
        {
            Vector3 hitPoint = hit.point;
            
            // Move particle system to welding point
            if (weldParticles != null)
            {
                weldParticles.transform.position = hitPoint;
                weldParticles.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                
                // Ensure particles are playing
                if (!weldParticles.isPlaying)
                {
                    weldParticles.Play();
                }
            }
            else
            {
                Debug.LogWarning("Weld particles not assigned!");
            }
            
            // Only add point if it's far enough from the last point (prevents overcrowding)
            bool shouldAddPoint = weldPoints.Count == 0 || 
                                  Vector3.Distance(hitPoint, weldPoints[weldPoints.Count - 1]) >= minPointDistance;
            
            if (shouldAddPoint)
            {
                weldPoints.Add(hitPoint);
                
                // Spawn weld mark at hit point
                GameObject weldMark = Instantiate(weldMarkPrefab, hitPoint, Quaternion.identity, weldMarksContainer.transform);
                weldMark.SetActive(true);
                weldMark.transform.localScale = Vector3.one * weldMarkSize;
                
                // Orient to surface normal
                weldMark.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                
                //Debug.Log($"Welding at: {hitPoint}, Total points: {weldPoints.Count}");
                
                // Check if hit point is near any incomplete knot
                CheckKnotCompletion(hitPoint);
            }
        }
        else
        {
            Debug.LogWarning("Raycast not hitting weld panel!");
        }
    }
    
    void CheckKnotCompletion(Vector3 weldPoint)
    {
        if (splineKnotPositions == null) return;
        
        for (int i = 0; i < splineKnotPositions.Length; i++)
        {
            if (knotsCompleted[i]) continue; // Skip already completed knots
            
            float distance = Vector3.Distance(weldPoint, splineKnotPositions[i]);
            
            if (distance <= knotCompletionRadius)
            {
                knotsCompleted[i] = true;
                //Debug.Log($"Knot {i} completed!");
                
                // Check if all knots are complete
                if (AreAllKnotsComplete())
                {
                    WeldComplete();
                }
            }
        }
    }
    
    bool AreAllKnotsComplete()
    {
        if (knotsCompleted == null) return false;
        
        foreach (bool completed in knotsCompleted)
        {
            if (!completed) return false;
        }
        
        return true;
    }
    
    void WeldComplete()
    {
        Debug.Log("Welding Complete!");
        isWelding = false;
        foreach (GameObject actor in completeActors)
        {
            actor.GetComponent<Renderer>().material = completedMaterial;
        }
        // Add your completion logic here
        // e.g., unlock door, trigger event, etc.
    }
    
    // Optional: Visualize knots in editor
    void OnDrawGizmos()
    {
        if (splineKnotPositions == null || splineKnotPositions.Length == 0) return;
        
        Gizmos.color = Color.yellow;
        foreach (Vector3 knotPos in splineKnotPositions)
        {
            Gizmos.DrawWireSphere(knotPos, knotCompletionRadius);
        }
    }
}
