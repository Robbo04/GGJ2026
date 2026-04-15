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

    //public Texture[] texture;

    void Start()
    {
        //reset crack counter to 0
        crackCount = 0;
    }

    void FixedUpdate()
    {
        //if all the windows have broken
        if(crackCount < 3)
        {
        //increasing the timer with delta time
        elapsedTime += Time.deltaTime;
        //if the timer exceeds 5 seconds
        if (elapsedTime > 5)
            {
                //SRAND if the window should break or not
                int randomNumber = Random.Range(0,3);
                print ("RandomNumber " + randomNumber);
                if (randomNumber == 2) //when SRAND is equal to 2 proceed to crack a window
                {
                    int randomCrack; //initialise SRAND variable
                    do //find a random window that is not cracked
                    {
                        randomCrack = Random.Range(0, CrackedWindow.Length); //SRAND find random window in gameobject list
                        print("RandomCrack " + randomCrack);
                    } while (CrackedWindow[randomCrack].GetComponent<Crack>().isCracked); //checks to see if window found is cracked
                    //window is found, now do this
                    CrackedWindow[randomCrack].GetComponent<Crack>().AddCracked(); //trigger add cracked function
                    //Trigger FMOD functionality.
                    crackSound = RuntimeManager.CreateInstance("event:/Minigame Oneshots/Cracking"); 
                    FmodAudioManager.Instance.PlayOneShot(crackSound, transform.position); 
                    Debug.Log("crackedSound");
                    
                    switch (CrackedWindow[randomCrack].GetComponent<Crack>().Health)    {
                        case 0: //Set window to be cracked material Default:
                        break;
                        case 1: //Set window to be cracked material light crack:
                        break;
                        case 2: //Set window to be cracked material heavy:
                        break;
                    }
                }
                elapsedTime = 0;
            }
        }

        //
    }

    void Update()
    {
        // Check how many cracks are currently active
        crackCount = 0;
        foreach (GameObject a in CrackedWindow)
        {
            if (a.GetComponent<Crack>().isCracked)
            {
                crackCount++;
            }
        }
        if (crackCount == 3)
        {
            print("Game over logic");
            SceneManager.LoadScene("MainLevel");
        }
    }


}
