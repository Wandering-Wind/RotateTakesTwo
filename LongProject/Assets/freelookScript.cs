using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class FreeLookRightStickControl : MonoBehaviour
{
    [Header("Input Settings")]
    public PlayerInput playerInput;
    public float rightStickSensitivity = 2f;

    private CinemachineFreeLook freeLookCamera;
    private Vector2 rightStickInput;

    void Start()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();

        // Disable mouse input for X axis
        freeLookCamera.m_XAxis.m_InputAxisName = "";
        freeLookCamera.m_XAxis.m_InputAxisValue = 0;

        // Optional: Adjust sensitivity
        freeLookCamera.m_XAxis.m_MaxSpeed = rightStickSensitivity;

        // Disable mouse input for X axis
        freeLookCamera.m_YAxis.m_InputAxisName = "";
        freeLookCamera.m_YAxis.m_InputAxisValue = 0;

        // Optional: Adjust sensitivity
        freeLookCamera.m_YAxis.m_MaxSpeed = rightStickSensitivity * Time.deltaTime;
    }

    void Update()
    {
        // Apply right stick input to X axis
        freeLookCamera.m_XAxis.m_InputAxisValue = rightStickInput.x;
        freeLookCamera.m_YAxis.m_InputAxisValue = rightStickInput.y;
    }

    // Call this from your PlayerController's input method
    public void OnRightStickLook(InputAction.CallbackContext context)
    {
        rightStickInput = context.ReadValue<Vector2>();
    }
}