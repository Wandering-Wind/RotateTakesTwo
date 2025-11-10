using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUICominucations : MonoBehaviour
{
    public LayerMask InteractLayer;
    [SerializeField]
    private List<string> Tags;
    public TextMeshProUGUI CommunicationText;
    public GameObject TextBox;
    private PlayerInput playerInputManager;
    public GameObject TextBox2;
    public GameObject TextBox3;
    public GameObject TextBox4;
    public TextMeshProUGUI CommunicationText2;
    public TextMeshProUGUI CommunicationText3;
    public TextMeshProUGUI CommunicationText4;

    private GameObject EndManager;
    private EndGameManager endGameManagerScript;
    public Transform PlayerCamera;
    [SerializeField]
    private int RayDistance;

    private void Start()
    {
        playerInputManager = GetComponent<PlayerInput>();
        EndManager = GameObject.FindGameObjectWithTag("Finish");
        endGameManagerScript = EndManager.GetComponent<EndGameManager>();
        
        if (playerInputManager.playerIndex == 0 )
        {
            TextBox = GameObject.FindGameObjectWithTag("TextBox");
            CommunicationText = TextBox.GetComponentInChildren<TextMeshProUGUI>();
            TextBox3 = GameObject.FindGameObjectWithTag("TextBox3");
            CommunicationText3 = TextBox3.GetComponentInChildren<TextMeshProUGUI>();
            TextBox3.SetActive(false);

        }
        else if (playerInputManager.playerIndex == 1)
        {
            TextBox2 = GameObject.FindGameObjectWithTag("TextBox2");
            CommunicationText2 = TextBox2.GetComponentInChildren<TextMeshProUGUI>();
            TextBox4 = GameObject.FindGameObjectWithTag("TextBox4");
            CommunicationText4 = TextBox4.GetComponentInChildren<TextMeshProUGUI>();
            TextBox4.SetActive(false);
        }


    }
    private void Update()
    {

        if (playerInputManager.playerIndex == 1)
        {
            Ray ray = new Ray(PlayerCamera.position, PlayerCamera.forward);
            RaycastHit hit;



            if (Physics.Raycast(ray, out hit, RayDistance, InteractLayer))
            {
                if (Tags.Contains(hit.collider.gameObject.name))
                {
                    if (hit.collider.gameObject.name == "Door")
                    {
                        if (endGameManagerScript.canOpenDoors)
                        {
                            CommunicationText2.text = "[" + hit.collider.gameObject.name + "]" + " R1/RB to Open";
                            TextBox2.SetActive(true);
                        }
                        else if (!endGameManagerScript.canOpenDoors)
                        {
                            CommunicationText2.text = "[Both players need to have Keys to Open]";
                            TextBox2.SetActive(true);
                        }
                    }
                    else if (hit.collider.gameObject.name == "Key2")
                    {
                        CommunicationText2.text = "[This is Player 1's Key]";
                        TextBox2.SetActive(true);
                    }
                    else
                    {
                        CommunicationText2.text = "[" + hit.collider.gameObject.name + "]" + " R2/RT to Push";
                        TextBox2.SetActive(true);
                    }
                }
            }
            else if (Physics.Raycast(ray, out hit, RayDistance, InteractLayer))
            {
                
            }
            else
            {
                TextBox2.SetActive(false);

            }
        }
        else if (playerInputManager.playerIndex == 0)
        {
            Ray ray = new Ray(PlayerCamera.position, PlayerCamera.forward);
            RaycastHit hit;



            if (Physics.Raycast(ray, out hit, 4, InteractLayer))
            {
                if (Tags.Contains(hit.collider.gameObject.name))
                {
                    if (hit.collider.gameObject.name == "Door")
                    {
                        if (endGameManagerScript.canOpenDoors)
                        {
                            CommunicationText.text = "[" + hit.collider.gameObject.name + "]" + " R1/RB to Open";
                            TextBox.SetActive(true);
                        }
                        else if (!endGameManagerScript.canOpenDoors)
                        {
                            CommunicationText.text = "[Both players need to have Keys to Open]";
                            TextBox.SetActive(true);
                        }
                    }
                    else if (hit.collider.gameObject.name == "Key")
                    {
                        CommunicationText.text = "[This is Player 2's Key]";
                        TextBox.SetActive(true);
                    }
                    else if (hit.collider.gameObject.name == "HandScanner")
                    {
                        CommunicationText.text = "[" +hit.collider.gameObject.name+ "]" + " R1/RB to Scan Hand";
                        TextBox.SetActive(true);
                    }
                    else { 
                    CommunicationText.text = "[ Need Other Player ]";
                    TextBox.SetActive(true);
                }
                }
            }
            else if (Physics.Raycast(ray, out hit, 4, InteractLayer))
            {
                
            }
            else
            {
                TextBox.SetActive(false);

            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerInputManager.playerIndex == 0)
        {
            if (other.CompareTag("Key"))
            {
                endGameManagerScript.KeysCollected[playerInputManager.playerIndex] = true;
                Destroy(other.gameObject);
            }
        }
        if (playerInputManager.playerIndex == 1)
        {
            if (other.CompareTag("Key2"))
            {
                endGameManagerScript.KeysCollected[playerInputManager.playerIndex] = true;
                Destroy(other.gameObject);
            }
        }
    }
}

