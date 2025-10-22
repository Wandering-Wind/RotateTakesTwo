using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Vector2 moveInput;
    private Rigidbody rb;
    [SerializeField]
    private List<ParticleSystem> player1Particles;
    [SerializeField]
    private List<TrailRenderer> Player1Trails, Player2Trails;
    



    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 10f;
    [SerializeField]
    private int JumpForceForBigPlayer;

    [Header("Ground Detection")]
    [SerializeField] private float raycastDistance = 1f;
    [SerializeField] private LayerMask groundLayer, InteractLayer;
    private PlayerInput playerInput;
    [SerializeField]
    private bool isGrounded;
    private int distance = 2;

    public CoinManager cm;

    [SerializeField]
    private bool isBigPlayer, isSmallPlayer;
    private GameObject HeldObject;
    [SerializeField]
    private int rotateSpeed;
    [SerializeField]
    private float turnInput;
    [SerializeField]
    private int MaxSpeed, normalSpeed, sprintSpeed;

    private bool isPushing;
    [SerializeField]
    private GameObject[] PlayerGameObject;
    [SerializeField]
    private GameObject rotateManager;
    [SerializeField]
    private RotateManager RotateManagerScript;
    [SerializeField]
    private SwapManager SwapManagerScript;
    private GameObject SwapManagerGO;
    [SerializeField]
    private List<GameObject> StartPositions;
    private GameObject JoinPanel;

    private GameObject UIControlGO;
    private UIControl UIcontrolsScript;
    [SerializeField]
    private List<Animator> PlayerAnimations;
    private bool inPortal;

    private GameObject EndManager;
    private EndGameManager endGameManagerScript;


    private PlayerUICominucations UIcontrol;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void HandleRotation()
    {
        // Convert the float rotation amount to a Quaternion
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * turnInput * rotateSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }


    private void Start()
    {
        rotateManager = GameObject.FindGameObjectWithTag("RotateManager");
        SwapManagerGO = GameObject.FindGameObjectWithTag("SwapManager");
        RotateManagerScript = rotateManager.GetComponent<RotateManager>();
        SwapManagerScript = SwapManagerGO.GetComponent<SwapManager>();
        UIControlGO = GameObject.FindGameObjectWithTag("UIcontrols");
        UIcontrolsScript = UIControlGO.GetComponent<UIControl>();
        UIcontrol = GetComponent<PlayerUICominucations>();


        EndManager = GameObject.FindGameObjectWithTag("Finish");
        endGameManagerScript = EndManager.GetComponent<EndGameManager>();
        if (playerInput.playerIndex == 0)
        {
            SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
            isSmallPlayer = true;
            MeshFilter PlayerMesh = GetComponent<MeshFilter>();
            Destroy(PlayerGameObject[1]);
            RotateManagerScript.Players.Add(transform);
            transform.tag = "Player1";
            SwapManagerScript.Players.Add(transform);
            StartPositions[0] = GameObject.FindGameObjectWithTag("P1Position");
            transform.position = StartPositions[0].transform.position;
            JoinPanel = GameObject.FindGameObjectWithTag("JoinPanel1");
            Destroy(JoinPanel);

        }
        else if (playerInput.playerIndex == 1)
        {
            SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
            isBigPlayer = true;
            MeshFilter PlayerMesh = GetComponent<MeshFilter>();
            Destroy(PlayerGameObject[0]);
            RotateManagerScript.Players.Add(transform);
            transform.tag = "Player2";
            SwapManagerScript.Players.Add(transform);
            StartPositions[1] = GameObject.FindGameObjectWithTag("P2Position");
            transform.position = StartPositions[1].transform.position;
            AudioListener AL = GetComponent<AudioListener>();
            Destroy(AL);
            JoinPanel = GameObject.FindGameObjectWithTag("JoinPanel2");
            Destroy(JoinPanel);

        }


    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // Changed to Vector2
    }


    public void OnRotateLeft(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            RotateManagerScript.RotateLeft[playerInput.playerIndex] = true;
        }
        else if (context.canceled)
        {
            if (!RotateManagerScript.isRotating)
            {
                RotateManagerScript.RotateLeft[playerInput.playerIndex] = false;
            }

        }
    }

    public void OnRotateRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RotateManagerScript.RotateRight[playerInput.playerIndex] = true;
        }
        else if (context.canceled)
        {
            if (!RotateManagerScript.isRotating)
            {
                RotateManagerScript.RotateRight[playerInput.playerIndex] = false;
            }

        }
    }
    public void OnAbility(InputAction.CallbackContext context)
    {
        
        if (isBigPlayer)
        {
            for (int i = 0; i < player1Particles.Count; i++)
            {
                player1Particles[i].Play();
            }
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (context.started)
            {
                isPushing = true;
                if (Physics.Raycast(ray, out hit, 2, InteractLayer))
                {
                    if (hit.collider.CompareTag("HeavyBlock"))
                    {

                        HeldObject = hit.collider.gameObject;
                        HeldObject.transform.parent = transform;
                        HeldObject.transform.position = new Vector3(transform.position.x, HeldObject.transform.position.y, HeldObject.transform.position.z);
                    }
                }
            }
            else if (context.canceled)
            {
                isPushing = false;
                if (Physics.Raycast(ray, out hit, 2, InteractLayer))
                {
                    if (hit.collider.CompareTag("HeavyBlock"))
                    {
                        HeldObject = hit.collider.gameObject;
                        HeldObject.AddComponent<Rigidbody>();
                        Rigidbody rb = HeldObject.GetComponent<Rigidbody>();
                        Destroy(rb, 0.2f);

                        HeldObject.transform.parent = null;
                        HeldObject = null;


                    }
                }
            }
        }
        else if (isSmallPlayer)
        {
            
            if (context.started)
            {
               if(speed < MaxSpeed)
                {
                    speed +=3;
                    for (int i = 0; i < Player2Trails.Count; i++)
                    {
                        Player2Trails[i].time = 0.1f;
                    }
                }
            }
            else if (context.canceled)
            {
                speed = normalSpeed;
                for (int i = 0; i < Player2Trails.Count; i++)
                {
                    Player2Trails[i].time = 0;
                }
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            Jump();
        }
    }

    void Update()
    {
        CheckGrounded();

        float currentAngle = transform.eulerAngles.z;
        
        if (playerInput.playerIndex == 0)
        {
            if (moveInput.x > 0 || moveInput.y > 0)
            {
                PlayerAnimations[0].SetBool("Move", true);
            }
            else if (moveInput.x == 0 && moveInput.y == 0)
            {
                PlayerAnimations[0].SetBool("Move", false);

            }
        }
        else if (playerInput.playerIndex == 1)
        {
            if (moveInput.x > 0 || moveInput.y > 0)
            {
                PlayerAnimations[1].SetBool("Move", true);
                player1Particles[2].Play();
                player1Particles[3].Play();
            }
            else if (moveInput.x == 0 && moveInput.y == 0)
            {
                PlayerAnimations[1].SetBool("Move", false);
                player1Particles[2].Stop();
                player1Particles[3].Stop();


            }
        }
        if (!RotateManagerScript.isRotating)
        {
            rb.useGravity = true;

            if (currentAngle == 0)
            {
                //down
                Physics.gravity = new Vector3(0, -9.81f, 0);
            }
            else if (currentAngle == -180)
            {
                //Up
                Physics.gravity = new Vector3(0, 9.81f, 0);

            }
            else if (currentAngle == -90)
            {
                //Left
                Physics.gravity = new Vector3(-9.81f,0, 0);

            }
            else if (currentAngle == 90)
            {
                //right
                Physics.gravity = new Vector3(9.81f, 0, 0);

            }
        }
        else if (RotateManagerScript.isRotating)
        {
            rb.useGravity = false;
        }
    }

   

    void FixedUpdate()
    {
      
        if (!isPushing)
        {
            HandleRotation();
        }

        
            // Apply velocity for movement
            // Create a new velocity vector based on moveInput, but preserve the current Y velocity
            Vector3 velocity = new Vector3(moveInput.x * speed, rb.velocity.y, moveInput.y * speed);

            // Transform the input direction from local space to world space based on player's current rotation
            Vector3 relativeVelocity = transform.TransformDirection(velocity);

            // Set the Rigidbody's velocity directly, preserving the y-axis for jumping
            rb.velocity = new Vector3(relativeVelocity.x, rb.velocity.y, relativeVelocity.z);

            // Call the new rotation function if there is movement input

        

    }

   

    public void OnInteract(InputAction.CallbackContext context)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit, 3, InteractLayer))
        {
            if (hit.collider.CompareTag("Door"))
            {
                if (endGameManagerScript.canOpenDoors)
                {
                    Destroy(hit.collider.gameObject);
                    endGameManagerScript.DoorsOpened[playerInput.playerIndex] = true;
                }
                //Door Open Script Here
            }
        }
    }

    public void OnSwap(InputAction.CallbackContext context)
    {
        if (playerInput.playerIndex == 0)
        {
            if (context.performed)
            {
                if (inPortal)
                {
                    SwapManagerScript.CanSwap[playerInput.playerIndex] = true;
                }
                else if(!inPortal)
                {
                    UIcontrol.TextBox3.SetActive(true);
                    UIcontrol.CommunicationText3.text = "[Use portal to swap!!]";
                }
            }
            else if (context.canceled)
            {
                SwapManagerScript.CanSwap[playerInput.playerIndex] = false;
                UIcontrol.TextBox3.SetActive(false);
            }
        }
        else if (playerInput.playerIndex == 1)
        {
            if (context.performed)
            {
                if (inPortal)
                {
                    SwapManagerScript.CanSwap[playerInput.playerIndex] = true;
                }
                else if(!inPortal)
                {
                   
                    UIcontrol.TextBox4.SetActive(true);
                    UIcontrol.CommunicationText4.text = "[Use portal to swap!!]";
                }
            }
            else if (context.canceled)
            {
                SwapManagerScript.CanSwap[playerInput.playerIndex] = false;
                UIcontrol.TextBox4.SetActive(false);
            }
        }
    }
    public void OnRotate(InputAction.CallbackContext context)
    {
        turnInput = context.ReadValue<float>();
    }

    void CheckGrounded()
    {
        // Use 3D raycast since we're using Rigidbody (3D)
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance, groundLayer);

        // Visual debug
        if (isGrounded)
        {
            Debug.DrawRay(transform.position, Vector3.down * distance, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * raycastDistance, Color.red);
        }
    }

    void Jump()
    {
       if (isBigPlayer)
        {
            rb.AddForce(transform.up * JumpForceForBigPlayer, ForceMode.Impulse);
        }
       else if (isSmallPlayer)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            cm.coinCounter++;
        }
        else if (other.CompareTag("KillBox"))
        {
            string Currentscene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(Currentscene);
            
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            inPortal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            inPortal = false;
        }
    }
    public void Pauseandplay(InputAction.CallbackContext context)
    {
        UIcontrolsScript.PauseandPlay();
    }
}