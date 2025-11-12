using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [Header("World Space Text")]
    [SerializeField] private GameObject interactText;

    [Header("Which Player Can Trigger")]
    [SerializeField] private string allowedPlayerTag = "Player1";

    private void Start()
    {
        if (interactText != null)
            interactText.SetActive(false); // Hide text at start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(allowedPlayerTag))
        {
            if (interactText != null)
                interactText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(allowedPlayerTag))
        {
            if (interactText != null)
                interactText.SetActive(false);
        }
    }
}
