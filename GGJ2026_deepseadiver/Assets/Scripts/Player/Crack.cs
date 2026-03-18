using UnityEngine;

public class Crack : MonoBehaviour
{
    public bool isCracked = false; // Flag to track if the helmet is cracked
    
    public void RemoveCracked()
    {
        if(isCracked)
        {
            isCracked = false;
            print("Crack removed");
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }

    public void AddCracked()
    {
        if (!isCracked)
        {
            isCracked = true;
            print("Crack added");
            // Add crack logic
            GetComponent<Renderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
            //FMOD Crack sound play Front Side
        }
    }
}
