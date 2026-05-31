using UnityEngine;

public class Crack : MonoBehaviour
{
    public bool isCracked = false; // Flag to track if the helmet is cracked
    public int Health = 2;
    
    public void RemoveCracked()
    {
        // Reset the crack state regardless of current isCracked status
        isCracked = false;
        Health = 2;
        Debug.Log($"Crack removed from: {gameObject.name}");
        GetComponent<Collider>().enabled = false;
        
        // Reset this window's material to default
        HelmetCrack helmetCrack = FindObjectOfType<HelmetCrack>();
        if (helmetCrack != null)
        {
            Renderer windowRenderer = GetComponent<Renderer>();
            if (windowRenderer != null)
            {
                windowRenderer.sharedMaterial = helmetCrack.DefaultWindowMat;
            }
        }
    }

    public void AddCracked()
    {
        // Don't add more cracks if already fully broken
        if (isCracked)
        {
            Debug.Log("Window already fully cracked, ignoring AddCracked");
            return;
        }
        
        Health -= 1;  // Fixed: was "Health =- 1" which assigns -1 instead of subtracting
        Debug.Log($"Window damaged. Health now: {Health}");
        //
        if (Health <= 0)  // Also changed to <= for safety
        {
            DestroyWindow();
        }
        else
        {
            print("Crack added");
            // Add crack logic
            GetComponent<Collider>().enabled = true;
            //FMOD Crack sound play Front Side
        }
    }
        

    public void DestroyWindow()
    {
        isCracked = true;
        GetComponent<Collider>().enabled = false;
        Debug.Log($"Window fully destroyed: {gameObject.name}");
        //audio break
        //visual cue here
    }
}
