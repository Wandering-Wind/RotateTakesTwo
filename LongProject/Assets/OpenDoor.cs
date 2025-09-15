using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Found Button to door");
    }
}
