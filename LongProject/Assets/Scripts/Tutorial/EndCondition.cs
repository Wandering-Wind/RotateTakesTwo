using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCondition : MonoBehaviour
{
    [Header("Next Scene")]
    [SerializeField] private string nextSceneName;

    [Header("Tutorial Key Tags")]
    [SerializeField] private string key1Tag = "Key";
    [SerializeField] private string key2Tag = "Key2";

    [Header("Allowed Player Tag")]
    [SerializeField] private string allowedPlayerTag = "Player1"; // Set per door in inspector

    private bool key1Collected = false;
    private bool key2Collected = false;
    private bool doorOpened = false;

    private void OnTriggerEnter(Collider other)
    {
        // Collect keys automatically when any player touches them
        if (other.CompareTag(key1Tag))
        {
            key1Collected = true;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag(key2Tag))
        {
            key2Collected = true;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (doorOpened) return;

        // Only the allowed player can interact
        if (!other.CompareTag(allowedPlayerTag))
            return;

        // Check if both keys are collected
        if (key1Collected && key2Collected)
        {
            // Use InputSystem interact button check
            if (other.TryGetComponent(out UnityEngine.InputSystem.PlayerInput pi))
            {
                var interactAction = pi.actions["InterAct"];
                if (interactAction.WasPressedThisFrame())
                {
                    OpenDoor();
                }
            }
        }
        else
        {
            // Optional: show hint UI like "Collect both keys first"
        }
    }

    private void OpenDoor()
    {
        doorOpened = true;

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
