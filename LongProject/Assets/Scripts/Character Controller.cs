using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Vector2 moveInput;
    private Rigidbody rb;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Detection")]
    [SerializeField] private float raycastDistance = 1f;
    [SerializeField] private LayerMask groundLayer;
    private PlayerInput playerInput;
    [SerializeField]
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
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
        // Movement - preserve Y velocity for jumping
        rb.velocity = new Vector3(moveInput.x * speed, rb.velocity.y, moveInput.y * speed);
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
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        Debug.Log("Jump!");
    }
}