using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject Player1, Player2;
    [SerializeField] private PlayerInputManager playerInputManager;
    private bool canSearchForPlayers = true;
    private GameObject inputManagerGameObject;

    [SerializeField] private float highlightDistance = 5f;

    private Outline outline; // your 3D outline script
    

    private void Start()
    {
        inputManagerGameObject = GameObject.FindGameObjectWithTag("GameController");
        if (inputManagerGameObject != null)
            playerInputManager = inputManagerGameObject.GetComponent<PlayerInputManager>();

        // Get or add Outline component, keep disabled by default
        outline = GetComponent<Outline>();
        if (outline == null)
            outline = gameObject.AddComponent<Outline>();
        outline.enabled = false;

        
    }

    private void Update()
    {
        if (canSearchForPlayers)
            CheckForPlayers();

        if (playerInputManager != null && playerInputManager.playerCount == 2 && Player1 && Player2)
        {
            canSearchForPlayers = false;

            float distance1 = Vector3.Distance(transform.position, Player1.transform.position);
            float distance2 = Vector3.Distance(transform.position, Player2.transform.position);

            bool nearPlayer1 = distance1 <= highlightDistance;
            bool nearPlayer2 = distance2 <= highlightDistance;

            bool isNearPlayer = nearPlayer1 || nearPlayer2;
            outline.enabled = isNearPlayer;

            if (isNearPlayer)
            {
                // Choose color based on who’s closer
                if (distance1 < distance2)
                    outline.OutlineColor = Color.blue; // Player 1
                else
                    outline.OutlineColor = Color.red;  // Player 2
            }
        }
    }

    private void CheckForPlayers()
    {
        Player1 = GameObject.FindGameObjectWithTag("Player1");
        Player2 = GameObject.FindGameObjectWithTag("Player2");
    }
}
