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
    public GameObject[] CrackedWindow; //list of gameobjects
    int crackCount;

    float elapsedTime;

    //public Texture[] texture;

    void Start()
    {
        crackCount = 0;
    }

    void FixedUpdate()
    {
        if(crackCount < 3)
        {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 5)
            {
                int randomNumber = Random.Range(0,3);
                print ("RandomNumber " + randomNumber);
                if (randomNumber == 2)
                {
                    int randomCrack;
                    do
                    {
                        randomCrack = Random.Range(0, CrackedWindow.Length);
                        print("RandomCrack " + randomCrack);
                    } while (CrackedWindow[randomCrack].GetComponent<Crack>().isCracked);
                    CrackedWindow[randomCrack].GetComponent<Crack>().AddCracked();
                }
                elapsedTime = 0;
            }
        }
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
