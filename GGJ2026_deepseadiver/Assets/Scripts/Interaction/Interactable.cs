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
    // [SerializeField]  public GameObject FrontPlane;
    // [SerializeField] public GameObject LeftPlane;
    // [SerializeField] public GameObject RightPlane;


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
}
