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

    public void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }
    public void EnableOutline()
    {
        outline.enabled = true;
    }

    public void Interact()
    {
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
