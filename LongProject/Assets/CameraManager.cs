using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class MultiplayerCameraManager : MonoBehaviour
{
    [Header("Camera References")]
    public CinemachineFreeLook[] playerCameras;

    [Header("Split Screen Settings")]
    public bool enableSplitScreen = true;
    public bool verticalSplit = false;

    private int activePlayers = 0;

    void Start()
    {
        // Initially disable all cameras except first
        for (int i = 0; i < playerCameras.Length; i++)
        {
            if (playerCameras[i] != null)
            {
                playerCameras[i].Priority = i == 0 ? 10 : 0;
                playerCameras[i].gameObject.SetActive(i == 0);
            }
        }
    }

    public void AssignCameraToPlayer(PlayerInput playerInput, Transform playerTransform)
    {
        int playerIndex = playerInput.playerIndex;

        if (playerIndex < playerCameras.Length && playerCameras[playerIndex] != null)
        {
            // Set up the camera for this player
            playerCameras[playerIndex].Follow = playerTransform;
            playerCameras[playerIndex].LookAt = playerTransform;
            playerCameras[playerIndex].Priority = 10;
            playerCameras[playerIndex].gameObject.SetActive(true);

            // Set up camera input control
            FreeLookRightStickControl cameraControl = playerCameras[playerIndex].GetComponent<FreeLookRightStickControl>();
            if (cameraControl != null)
            {
                cameraControl.playerInput = playerInput;
            }

            activePlayers++;
            UpdateCameraViewports();
        }
    }

    void UpdateCameraViewports()
    {
        if (!enableSplitScreen || activePlayers <= 1)
        {
            // Single screen - only first camera visible
            for (int i = 0; i < playerCameras.Length; i++)
            {
                if (playerCameras[i] != null)
                {
                    Camera cam = playerCameras[i].GetComponent<Camera>();
                    if (cam != null)
                    {
                        cam.rect = i == 0 ? new Rect(0, 0, 1, 1) : new Rect(0, 0, 0, 0);
                    }
                }
            }
        }
        else
        {
            // Split screen setup
            for (int i = 0; i < activePlayers; i++)
            {
                if (playerCameras[i] != null)
                {
                    Camera cam = playerCameras[i].GetComponent<Camera>();
                    if (cam != null)
                    {
                        if (verticalSplit)
                        {
                            // Vertical split (side by side)
                            float width = 1f / activePlayers;
                            cam.rect = new Rect(i * width, 0, width, 1);
                        }
                        else
                        {
                            // Horizontal split (top and bottom)
                            float height = 1f / activePlayers;
                            cam.rect = new Rect(0, 1 - ((i + 1) * height), 1, height);
                        }
                    }
                }
            }
        }
    }
}