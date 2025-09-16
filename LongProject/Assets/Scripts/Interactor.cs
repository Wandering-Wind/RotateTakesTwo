using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public float interactRange;
    public LayerMask interactiveLayer;
    private IInteractable interactable;
    //ray debugging
    public float rayLength = 10f;
    public Color rayColor = Color.red;


    public void onInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange, interactiveLayer))
        {
            interactable = hit.collider.GetComponent<IInteractable>();
            Debug.DrawRay(transform.position, transform.forward,rayColor);
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}
