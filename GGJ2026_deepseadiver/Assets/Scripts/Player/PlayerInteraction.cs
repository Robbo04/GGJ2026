// using UnityEngine;
// using System.Collections.Generic;
// using System.Collections;

// public class PlayerInteraction : MonoBehaviour
// {
//     public float reachRange = 3f;
//     Interactable currentInteractable;

//     // Update is called once per frame
//     void Update()
//     {
//         CheckInteraction();
//         if (Input.GetKeyDown(KeyCode.E))
//         {
//             if (currentInteractable != null)
//             {
//                 currentInteractable.Interact();
//             }
//         }
//     }

//     void CheckInteraction()
//     {
//         RaycastHit hit;
//         Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
//         if (Physics.Raycast(ray, out hit, reachRange))
//         {
//             if (hit.collider.tag == "Interactable")
//             {
//                 Interactable newInteractable = hit.collider.GetComponent<Interactable>();

//                 if (newInteractable.enabled)
//                 {
//                     SetNewInteractable(newInteractable);
//                 }

//             }
//             else
//             {
//                 ClearCurrentInteractable();
//             }
//         }
//     }


//     void SetNewInteractable(Interactable newInteractable)
//     {
//         currentInteractable = newInteractable;
//         currentInteractable.EnableOutline();
//     }

//     void ClearCurrentInteractable()
//     {
//         if (currentInteractable)
//         {
//             currentInteractable.DisableOutline();
//             currentInteractable = null;
//         }
//     }
// }