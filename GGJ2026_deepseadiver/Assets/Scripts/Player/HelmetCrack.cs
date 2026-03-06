using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using Unity.VisualScripting;
using NUnit.Framework;

public class HelmetCrack : MonoBehaviour
{

    private Renderer FrontRend;
    private Renderer LeftRend;
    private Renderer RightRend;
    private int randomTextureIndex;


    [SerializeField] private WindowName windowName;

    int randomNumber;
    int randomCrackObject;

    private bool isFrontCracked = false;
    private bool isLeftCracked = false;
    private bool isRightCracked = false;

    public Texture[] texture;
    float elapsedTime;
    

    public GameObject FrontPlane;
    public GameObject LeftPlane;
    public GameObject RightPlane;

    void Start ()
    {
        FrontRend = FrontPlane.GetComponent<Renderer>();
        LeftRend = LeftPlane.GetComponent<Renderer>();
        RightRend = RightPlane.GetComponent<Renderer>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (isFrontCracked && isLeftCracked && isRightCracked)
        {
            print ("Death Condition Met");
        }
        else
        {
            if (elapsedTime > 5)
            {
                randomNumber = Random.Range(0,4);
                if (randomNumber == 2)
                {
                    randomCrackObject = Random.Range(0,3);
                    if (randomCrackObject == (int) WindowName.FrontWindow)
                    {
                        if (!isFrontCracked)
                        {
                            print("FrontWindowCrack");
                            //FMOD Crack sound play Front Side
                            elapsedTime = 0;
                            isFrontCracked = true;
                        }                  
                    }
                    else if (randomCrackObject == (int) WindowName.LeftWindow)
                    {
                        if (!isLeftCracked)
                        {
                            print("Left Window Crack");
                            //FMOD Crack sound play Left Side
                            elapsedTime = 0;
                            isLeftCracked = true;
                        }                
                    }
                    else if (randomCrackObject == (int) WindowName.RightWindow)
                    {
                        if (!isRightCracked)
                        {
                            print ("Right Window Crack");
                            //FMOD Crack sound play Right Side
                            elapsedTime = 0;
                            isRightCracked = true;
                        }  
                    }
                
                
                }
            
            }
        }

       
        if (randomNumber < 5)
        {
            elapsedTime = 0;
        }
    }

}
