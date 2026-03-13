using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using Unity.VisualScripting;

public class Interactable : MonoBehaviour
{
    Outline outline;
    public string message;
    public UnityEvent interactEvent;

    [SerializeField] public GameObject QTEManager;
    [SerializeField]  public GameObject FrontPlane;
    [SerializeField] public GameObject LeftPlane;
    [SerializeField] public GameObject RightPlane;


    public void Start()
    {
        outline = GetComponent<Outline>();
        if (outline != null)
        {
            DisableOutline();
        }
    }

    public void DisableOutline()
    {
        if (outline != null)
        {
            outline.enabled = false;
        }
    }
    
    public void EnableOutline()
    {
        if (outline != null)
        {
            outline.enabled = true;
        }
    }

    public void Interact()
    {
        print("FrontInteracted");
        interactEvent.Invoke();
    }


    public void DestroyThis()
    {
        Destroy(gameObject);
        Debug.Log("Destroyed ");
    }

    public void StartMinigame()
    {
        QTEManager.GetComponent<KeySelector>().enabled = true;
        DestroyThis();
        Debug.Log("Minigame Started");
    }

    public void RemoveFrontWindowCrack()
    {
        FrontPlane.GetComponent<Renderer>().enabled = !FrontPlane.GetComponent<Renderer>().enabled;
        FrontPlane.GetComponent<BoxCollider>().enabled = !FrontPlane.GetComponent<BoxCollider>().enabled;
        FrontPlane.GetComponent<HelmetCrack>().isFrontCracked =  !FrontPlane.GetComponent<HelmetCrack>().isFrontCracked;
    }

    public void RemoveLeftWindowCrack()
    {
        LeftPlane.GetComponent<Renderer>().enabled = !LeftPlane.GetComponent<Renderer>().enabled;
        LeftPlane.GetComponent<BoxCollider>().enabled = !LeftPlane.GetComponent<BoxCollider>().enabled;
        LeftPlane.GetComponent<HelmetCrack>().isLeftCracked =  !LeftPlane.GetComponent<HelmetCrack>().isLeftCracked;
    }

    public void RemoveRightWindowCrack()
    {
        RightPlane.GetComponent<Renderer>().enabled = !RightPlane.GetComponent<Renderer>().enabled;
        RightPlane.GetComponent<BoxCollider>().enabled = !RightPlane.GetComponent<BoxCollider>().enabled;
        RightPlane.GetComponent<HelmetCrack>().isRightCracked =  !RightPlane.GetComponent<HelmetCrack>().isRightCracked;
    }
}
