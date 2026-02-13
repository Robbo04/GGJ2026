using UnityEngine;
using UnityEngine.Splines;
using System.Collections;

public class SplineMovementManager : MonoBehaviour
{
    [Header("Spline Settings")]
    public SplineContainer splineContainer;    // Reference to SplineContainer
    public Transform objectToMove;             // Object to move along spline
    public float movementSpeed = 1f;           // Speed of movement along spline
    public bool rotateAlongSpline = false;     // Whether to rotate object along spline direction
    
    private bool isMoving = false;
    private int currentKnotIndex = 0;
    private int totalKnots;
    private float currentT = 0f;               // Current normalized position (0 to 1)
    
    void Start()
    {
        if (splineContainer != null && splineContainer.Spline != null)
        {
            totalKnots = splineContainer.Spline.Count;
            
            // Position object at start of spline
            if (objectToMove != null)
            {
                Vector3 localPos = splineContainer.Spline.EvaluatePosition(0f);
                objectToMove.position = splineContainer.transform.TransformPoint(localPos);
            }
        }
    }
    
    // Call this function to move to the next knot
    public void MoveToNextKnot()
    {
        if (splineContainer == null || objectToMove == null || isMoving)
        {
            Debug.LogWarning("Cannot move - either no SplineContainer/objectToMove assigned or already moving");
            return;
        }
        
        if (currentKnotIndex >= totalKnots - 1)
        {
            Debug.Log("Already at the last knot");
            return;
        }
        
        StartCoroutine(MoveToNextKnotCoroutine());
    }
    
    private IEnumerator MoveToNextKnotCoroutine()
    {
        isMoving = true;
        currentKnotIndex++;
        
        // Get the actual curve length up to the target knot
        float splineLength = splineContainer.Spline.GetLength();
        float targetDistance = splineContainer.Spline.GetCurveLength(currentKnotIndex - 1);
        for (int i = 0; i < currentKnotIndex; i++)
        {
            if (i > 0)
            {
                targetDistance += splineContainer.Spline.GetCurveLength(i - 1);
            }
        }
        float targetT = targetDistance / splineLength;
        
        Debug.Log($"Moving from knot {currentKnotIndex - 1} to knot {currentKnotIndex}, targetT: {targetT}");
        
        // Move along spline with threshold check
        while (Mathf.Abs(currentT - targetT) > 0.001f) // Small threshold for stopping
        {
            currentT += movementSpeed * Time.deltaTime / splineLength;
            currentT = Mathf.Clamp(currentT, 0f, targetT);
            
            // Update position
            Vector3 localPos = splineContainer.Spline.EvaluatePosition(currentT);
            objectToMove.position = splineContainer.transform.TransformPoint(localPos);
            
            // Optional: update rotation
            if (rotateAlongSpline)
            {
                Vector3 forward = splineContainer.Spline.EvaluateTangent(currentT);
                if (forward != Vector3.zero)
                {
                    objectToMove.rotation = splineContainer.transform.rotation * Quaternion.LookRotation(forward);
                }
            }
            
            yield return null;
        }
        
        // Snap to exact knot position
        Vector3 finalLocalPos = splineContainer.Spline.EvaluatePosition(targetT);
        objectToMove.position = splineContainer.transform.TransformPoint(finalLocalPos);
        currentT = targetT;
        
        isMoving = false;
        Debug.Log($"Reached knot {currentKnotIndex} at t={currentT}");
    }
    
    // Optional: Move to a specific knot index
    public void MoveToKnot(int knotIndex)
    {
        if (knotIndex < 0 || knotIndex >= totalKnots)
        {
            Debug.LogWarning($"Invalid knot index: {knotIndex}");
            return;
        }
        
        if (splineContainer == null || objectToMove == null || isMoving)
        {
            return;
        }
        
        StartCoroutine(MoveToSpecificKnotCoroutine(knotIndex));
    }
    
    private IEnumerator MoveToSpecificKnotCoroutine(int targetKnot)
    {
        isMoving = true;
        
        float targetT = targetKnot / (float)(totalKnots - 1);
        
        Debug.Log($"Moving from knot {currentKnotIndex} to knot {targetKnot}");
        
        // Determine direction
        float direction = targetKnot > currentKnotIndex ? 1f : -1f;
        
        // Move along spline
        while ((direction > 0 && currentT < targetT) || (direction < 0 && currentT > targetT))
        {
            currentT += direction * movementSpeed * Time.deltaTime / splineContainer.Spline.GetLength();
            currentT = Mathf.Clamp01(currentT);
            
            // Update position
            Vector3 localPos = splineContainer.Spline.EvaluatePosition(currentT);
            objectToMove.position = splineContainer.transform.TransformPoint(localPos);
            
            // Optional: update rotation
            if (rotateAlongSpline)
            {
                Vector3 forward = splineContainer.Spline.EvaluateTangent(currentT);
                if (forward != Vector3.zero)
                {
                    objectToMove.rotation = splineContainer.transform.rotation * Quaternion.LookRotation(forward);
                }
            }
            
            yield return null;
            
            // Check if we've reached target
            if ((direction > 0 && currentT >= targetT) || (direction < 0 && currentT <= targetT))
            {
                break;
            }
        }
        
        currentKnotIndex = targetKnot;
        isMoving = false;
        
        Debug.Log($"Reached knot {currentKnotIndex}");
    }
}
