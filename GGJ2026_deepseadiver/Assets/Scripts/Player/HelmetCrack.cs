using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using Unity.VisualScripting;
using NUnit.Framework;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class HelmetCrack : MonoBehaviour
{

    // private Renderer FrontRend;
    // private Renderer LeftRend;
    // private Renderer RightRend;
    // private int randomTextureIndex;


    [SerializeField] private WindowName windowName;

    int randomNumber;
    int randomCrackObject;

    public bool isFrontCracked = false;
    public bool isLeftCracked = false;
    public bool isRightCracked = false;

    //public Texture[] texture;
    float elapsedTime;
    

    public GameObject FrontPlane;
    public GameObject LeftPlane;
    public GameObject RightPlane;

    void Start ()
    {
        FrontPlane.GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
        LeftPlane.GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
        RightPlane.GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
    }

    void Update()
    {
        if (elapsedTime == 5)
        {
                    print ("Time " + elapsedTime);
        }

        elapsedTime += Time.deltaTime;
        if (isFrontCracked && isLeftCracked && isRightCracked)
        {
            //Change later to add death screen menu
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
        else
        {
            if (elapsedTime > 5)
            {
                randomNumber = Random.Range(0,4);
                print ("RandomNumber " + randomNumber);
                if (randomNumber == 2)
                {
                    randomCrackObject = Random.Range(0,3);

                    print ("RandomCrackObject " + randomCrackObject);
                    if (randomCrackObject == (int) WindowName.FrontWindow)
                    {
                        if (!isFrontCracked)
                        {
                            print("FrontWindowCrack");
                            //FMOD Crack sound play Front Side
                            FrontPlane.GetComponent<Renderer>().enabled = GetComponent<Renderer>().enabled;
                            FrontPlane.GetComponent<BoxCollider>().enabled = FrontPlane.GetComponent<BoxCollider>().enabled;
                            elapsedTime = 0;
                            isFrontCracked = true;
                        }
                        else if (isFrontCracked)
                        {
                            randomCrackObject = Random.Range(1,2);
                        }                  
                    }
                    else if (randomCrackObject == (int) WindowName.LeftWindow)
                    {
                        if (!isLeftCracked)
                        {
                            print("Left Window Crack");
                            //FMOD Crack sound play Left Side
                            LeftPlane.GetComponent<Renderer>().enabled = GetComponent<Renderer>().enabled;
                            LeftPlane.GetComponent<BoxCollider>().enabled = LeftPlane.GetComponent<BoxCollider>().enabled;
                            elapsedTime = 0;
                            isLeftCracked = true;
                        }
                        else if (isLeftCracked)
                        {
                            randomCrackObject = Random.Range(0,2);
                            if (randomCrackObject == 1)
                            {
                                randomCrackObject = 2;
                            }
                        }       
                    }
                    else if (randomCrackObject == (int) WindowName.RightWindow)
                    {
                        if (!isRightCracked)
                        {
                            print ("Right Window Crack");
                            //FMOD Crack sound play Right Side
                            RightPlane.GetComponent<Renderer>().enabled = GetComponent<Renderer>().enabled;
                            RightPlane.GetComponent<BoxCollider>().enabled = RightPlane.GetComponent<BoxCollider>().enabled;
                            elapsedTime = 0;
                            isRightCracked = true;
                        }  
                        else if (isRightCracked)
                        {
                            randomCrackObject = Random.Range(0,1);
                        } 
                    }               
                }
                else
                {
                    elapsedTime = 0;
                }
            
            }
        }
    }

}
