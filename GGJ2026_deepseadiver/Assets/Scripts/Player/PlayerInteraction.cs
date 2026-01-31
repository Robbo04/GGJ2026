using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float reachRange = 3f;
    Interactable currentInteractable;
    
    private PlayerInputActions playerControls;

    void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    void OnEnable()
    {
        playerControls.PlayerController.Enable();
        playerControls.PlayerController.Interact.performed += OnInteract;
    }

    void OnDisable()
    {
        playerControls.PlayerController.Interact.performed -= OnInteract;
        playerControls.PlayerController.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckInteraction();
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, reachRange))
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                if (newInteractable.enabled)
                {
                    SetNewInteractable(newInteractable);
                }

            }
            else
            {
                ClearCurrentInteractable();
            }
        }
    }


    void SetNewInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
    }

    void ClearCurrentInteractable()
    {
        if (currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }
}