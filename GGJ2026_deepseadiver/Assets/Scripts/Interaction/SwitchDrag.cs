using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using FMOD.Studio;
using FMODUnity;

public class SwitchDrag : Interactable
{
    [SerializeField] public GameObject player;
    [Header("Lever Settings")]
    public Transform leverRoot;          // The parent object to rotate (leave empty to use parent)
    public CameraScript cameraScript;    // Reference to camera to lock during drag
    public float minAngle = 0f;          // Lever up position
    public float maxAngle = 45f;         // Lever down position (on)
    public float dragSensitivity = 0.5f; // How much drag affects rotation
    public float onThreshold = 40f;      // Angle at which lever is considered "on"

    [SerializeField] private EventReference leverSound;
    [SerializeField] private EventReference moveCrainSound;
    
    
    private PlayerInputActions playerControls;
    private bool isDragging = false;
    private bool isOn = false;
    private float currentAngle = 0f;
    private Vector2 lookInput;
    
    
    new void Start()
    {
        base.Start();
        
        // If leverRoot not assigned, use parent object
        if (leverRoot == null)
        {
            leverRoot = transform.parent;
        }
        
        playerControls = new PlayerInputActions();
        playerControls.PlayerController.Enable();
        playerControls.PlayerController.Look.performed += OnLook;
        playerControls.PlayerController.Look.canceled += OnLook;
        playerControls.PlayerController.Interact.canceled += OnInteractCanceled;
    }
    
    void OnDestroy()
    {
        if (playerControls != null)
        {
            playerControls.PlayerController.Look.performed -= OnLook;
            playerControls.PlayerController.Look.canceled -= OnLook;
            playerControls.PlayerController.Interact.canceled -= OnInteractCanceled;
            playerControls.PlayerController.Disable();
        }
    }
    
    // Called by PlayerInteraction system when interact button is pressed while looking at this
    new public void Interact()
    {
        isDragging = true;
        FmodAudioManager.Instance.PlayOneShot(leverSound, transform.position);
        // Lock camera while dragging
        if (cameraScript != null)
        {
            cameraScript.enabled = false;
        }
        
        base.Interact(); // Call base to invoke interactEvent if needed
    }
    
    private void OnInteractCanceled(InputAction.CallbackContext context)
    {
        isDragging = false;
        
        // Unlock camera when done dragging
        if (cameraScript != null)
        {
            cameraScript.enabled = true;
            //FmodAudioManager.Instance.StopOneShot(leverSound);
        }
    }
    
    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    
    void Update()
    {
        if (isDragging)
        {
            // Convert mouse Y movement to lever rotation
            // Negative because dragging down should increase angle
            float dragAmount = -lookInput.y * dragSensitivity;
            currentAngle += dragAmount;
            currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);
            
            // Apply rotation to the lever root (parent object)
            if (leverRoot != null)
            {
                leverRoot.localRotation = Quaternion.Euler(0f, 0f, currentAngle);
            }
            
            // Check state change
            CheckLeverState();
        }
    }
    
    void CheckLeverState()
    {
        bool wasOn = isOn;
        isOn = currentAngle >= onThreshold;
        
        if (isOn != wasOn)
        {
            // State changed, invoke event
            Interact();
            
            if (isOn)
            {
                PulledDown();
            }
        }
    }
    
    void PulledDown()
    {
        // Lever has been pulled down (toggled on)
        Debug.Log("Lever pulled down!");
        player.GetComponent<SplineMovementManager>().MoveToNextKnot(); 
        FmodAudioManager.Instance.PlayOneShot(moveCrainSound, transform.position);
        
    }
}