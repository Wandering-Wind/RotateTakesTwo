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


    private void Start()
    {
        playerInputManager = GetComponent<PlayerInput>();
        
        
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
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;



            if (Physics.Raycast(ray, out hit, 4, InteractLayer))
            {
                if (Tags.Contains(hit.collider.gameObject.name))
                {
                    CommunicationText2.text = "[" + hit.collider.gameObject.name + "]" + " R2/RT to Push";
                    TextBox2.SetActive(true);
                }
            }
            else
            {
                Debug.Log("None");
                TextBox2.SetActive(false);

            }
        }
        else if (playerInputManager.playerIndex == 0)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;



            if (Physics.Raycast(ray, out hit, 4, InteractLayer))
            {
                if (Tags.Contains(hit.collider.gameObject.name))
                {
                    CommunicationText.text = "[ Need Other Player ]";
                    TextBox.SetActive(true);
                }
            }
            else
            {
                Debug.Log("None");
                TextBox.SetActive(false);

            }
        }

    }
}
