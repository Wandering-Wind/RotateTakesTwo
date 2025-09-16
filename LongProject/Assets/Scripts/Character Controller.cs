using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Vector2 moveInput;
    private Rigidbody rb;

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

<<<<<<< Updated upstream
<<<<<<< Updated upstream
    //Coin Manager
    public CoinManager cm;
=======
=======
>>>>>>> Stashed changes
    [SerializeField]
    private bool isBigPlayer, isSmallPlayer;
    private GameObject HeldObject;
    [SerializeField]
    private int rotateSpeed;
    [SerializeField]
    private float turnInput;
    [SerializeField]
    private int MaxSpeed,normalSpeed, sprintSpeed;

    private bool isPushing;
    

<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
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
        if (playerInput.playerIndex == 0)
        {
            SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
            //gameObject.tag = "Player1";
        }
        else if (playerInput.playerIndex == 1)
        {
            SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
            //gameObject.tag = "Player2";
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // Changed to Vector2
    }

    public void OnAbility(InputAction.CallbackContext context)
    {
        if (isBigPlayer)
        {

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
                    }
                }
            }
            else if(context.canceled)
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
                }
            }
            else if (context.canceled)
            {
                speed = normalSpeed; 
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

    public void OnRotate(InputAction.CallbackContext context)
    {
        turnInput = context.ReadValue<float>();
    }

    void CheckGrounded()
    {
        // Use 3D raycast since we're using Rigidbody (3D)
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, groundLayer);

        // Visual debug
        if (isGrounded)
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.green);
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
            rb.AddForce(Vector3.up * JumpForceForBigPlayer, ForceMode.Impulse);
        }
       else if (isSmallPlayer)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }

<<<<<<< Updated upstream
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            cm.coinCounter++;
        }
=======
>>>>>>> Stashed changes
    }
}