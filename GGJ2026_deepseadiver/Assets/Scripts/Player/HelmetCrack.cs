using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using Unity.VisualScripting;
using NUnit.Framework;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using FMODUnity;

public class HelmetCrack : MonoBehaviour
{
    public GameObject[] CrackedWindow; //list of gameobjects
    
    public Material DefaultWindowMat;
    public Material MildCrackedWindowMat;
    public Material HeavyCrackedWindowMat;

    [SerializeField] public FMOD.Studio.EventInstance crackSound; //FMOD event for crack sound.
    int crackCount;
    float elapsedTime;
    private Crack[] crackComponents; // Cache crack components

    //public Texture[] texture;

    void Start()
    {
        //reset crack counter to 0
        crackCount = 0;
        
        // Cache all Crack components at start
        crackComponents = new Crack[CrackedWindow.Length];
        for (int i = 0; i < CrackedWindow.Length; i++)
        {
            crackComponents[i] = CrackedWindow[i].GetComponent<Crack>();
        }
    }

    void FixedUpdate()
    {
        // Update crack count at the start of FixedUpdate to avoid race conditions
        UpdateCrackCount();
        
        //if all the windows have broken
        if(crackCount < 3)
        {
        //increasing the timer with fixed delta time (correct for FixedUpdate)
        elapsedTime += Time.fixedDeltaTime;
        //if the timer exceeds 5 seconds
        if (elapsedTime > 5)
            {
                //SRAND if the window should break or not
                int randomNumber = Random.Range(0,3);
                print ("RandomNumber " + randomNumber);
                if (randomNumber == 2) //when SRAND is equal to 2 proceed to crack a window
                {
                    // Count how many windows are not cracked (use cached components)
                    int availableWindows = 0;
                    for (int i = 0; i < crackComponents.Length; i++)
                    {
                        if (crackComponents[i] != null && !crackComponents[i].isCracked)
                        {
                            availableWindows++;
                        }
                    }
                    
                    // Safety check - if no windows available, skip this frame
                    if (availableWindows == 0)
                    {
                        elapsedTime = 0;
                        return;
                    }
                    
                    int randomCrack = -1; //initialise SRAND variable
                    int attempts = 0; //prevent infinite loop
                    
                    // Find a random non-cracked window
                    do
                    {
                        randomCrack = Random.Range(0, crackComponents.Length);
                        print("RandomCrack " + randomCrack);
                        attempts++;
                        if (attempts > 100) // Emergency break
                        {
                            Debug.LogError("Failed to find available window after 100 attempts!");
                            elapsedTime = 0;
                            return;
                        }
                    } while (crackComponents[randomCrack] == null || crackComponents[randomCrack].isCracked);
                    //window is found, now do this
                    if (crackComponents[randomCrack] != null) // Final safety check
                    {
                        crackComponents[randomCrack].AddCracked(); //trigger add cracked function
                        //Trigger FMOD functionality.
                        crackSound = RuntimeManager.CreateInstance("event:/Minigame Oneshots/Cracking"); 
                        FmodAudioManager.Instance.PlayOneShot(crackSound, transform.position); 
                        Debug.Log("crackedSound");
                        
                        switch (crackComponents[randomCrack].Health)    {
                            case 0: //Set window to be cracked material Default:
                            break;
                            case 1: //Set window to be cracked material light crack:
                            break;
                            case 2: //Set window to be cracked material heavy:
                            break;
                        }
                    }
                }
                elapsedTime = 0;
            }
        }

        //
    }

    void Update()
    {
        // Just check for game over in Update
        if (crackCount >= 3)
        {
            print("Game over logic");
            SceneManager.LoadScene("MainLevel");
        }
    }
    
    void UpdateCrackCount()
    {
        if (crackComponents == null) return; // Safety check
        
        // Check how many cracks are currently active (use cached components)
        int newCrackCount = 0;
        for (int i = 0; i < crackComponents.Length; i++)
        {
            if (crackComponents[i] != null && crackComponents[i].isCracked)
            {
                newCrackCount++;
            }
        }
        
        // Only update if count changed (prevents unnecessary logs)
        if (newCrackCount != crackCount)
        {
            crackCount = newCrackCount;
            Debug.Log($"Crack count updated to: {crackCount}");
        }
    }


}
