using UnityEngine;

public class Crack : MonoBehaviour
{
    public bool isCracked = false; // Flag to track if the helmet is cracked
    public GameObject[] Windows;
    public int Health = 2;
    
    public void RemoveCracked()
    {
        if(!isCracked)
        {
            Health = 2;
            print("Crack removed");
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }

    public void AddCracked()
    {
        if (!isCracked)
        {
            Health =- 1;
            if (Health == 0)
            {
                DestroyWindow();
            }
            else
            {
                print("Crack added");
                // Add crack logic
                GetComponent<Renderer>().enabled = true;
                GetComponent<Collider>().enabled = true;
                //FMOD Crack sound play Front Side
            }
            
           
        }
    }

    public void DestroyWindow()
    {
        isCracked = true;
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = false;
        //audio break
        //visual cue here
    }
}
