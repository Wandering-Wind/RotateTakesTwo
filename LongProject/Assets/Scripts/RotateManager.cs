using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    public List<Transform> Levels;
    public List<Transform> Rotations;
    [SerializeField]
    private float RotateDuration = 1f;
    public List<bool> RotateRight;
    public List<bool> RotateLeft;
    [SerializeField]
    public bool isRotating;

    public List<Transform> Players;

    public List<GameObject> RotateRightUI;
    public List<GameObject> RotateLeftUI;

    public List<GameObject> RotateImageRight;
    public List<GameObject> RotateImageLeft;
    public List<GameObject> RotateUIBG;

    private Coroutine currentRotationCoroutine;

    public void Rotate()
    {
        if (!isRotating)
        {
            currentRotationCoroutine = StartCoroutine(RotateWorld());
        }
    }

    private void Update()
    {
        // Check if all players are rotating in the same direction
        if (RotateRight.All(b => b) && !isRotating)
        {
            Rotate();
        }
        else if (RotateLeft.All(b => b) && !isRotating)
        {
            Rotate();
        }

        UpdateUI();

        AnimateUI();

        // Only handle snapping when not rotating
        if (!isRotating)
        {
            SnapToNearest90Degrees();
        }
    }

    private void UpdateUI()
    {
        // Update right rotation UI
        if (RotateRight[0])
        {
            RotateRightUI[1].SetActive(true);
        }
        else
        {
            RotateRightUI[1].SetActive(false);
        }

        if (RotateRight[1])
        {
            RotateRightUI[0].SetActive(true);
        }
        else
        {
            RotateRightUI[0].SetActive(false);
        }

        // Update left rotation UI
        if (RotateLeft[0])
        {
            RotateLeftUI[1].SetActive(true);
        }
        else
        {
            RotateLeftUI[1].SetActive(false);
        }

        if (RotateLeft[1])
        {
            RotateLeftUI[0].SetActive(true);
        }
        else
        {
            RotateLeftUI[0].SetActive(false);
        }

        // Update UI backgrounds
        RotateUIBG[0].SetActive(RotateRight[0] || RotateLeft[0]);
        RotateUIBG[1].SetActive(RotateRight[1] || RotateLeft[1]);
    }

    private void AnimateUI()
    {
        // Animate rotation UI images
        RotateImageRight[0].transform.Rotate(0, 0, -100 * Time.deltaTime);
        RotateImageLeft[0].transform.Rotate(0, 0, 100 * Time.deltaTime);
        RotateImageLeft[1].transform.Rotate(0, 0, -100 * Time.deltaTime);
        RotateImageRight[1].transform.Rotate(0, 0, 100 * Time.deltaTime);
    }

    private void SnapToNearest90Degrees()
    {
        foreach (Transform t in Levels)
        {
            if (Rotations.Count == 2)
            {
                // Get the current Euler angle
                float currentAngle = t.eulerAngles.z;

                // Snap to nearest 90 degrees
                float snappedAngle = Mathf.Round(currentAngle / 90f) * 90f;

                // Apply the snapped rotation
                t.rotation = Quaternion.Euler(0, 0, snappedAngle);
            }
        }
    }

    IEnumerator RotateWorld()
    {
        isRotating = true;

        // Set rotation pivot points to player positions
        for (int i = 0; i < Players.Count && i < Rotations.Count; i++)
        {
            Rotations[i].transform.position = Players[i].transform.position;
            Levels[i].parent = Rotations[i];
        }

        // Determine rotation direction and amount
        float targetRotation = 0f;
        if (RotateRight.All(b => b))
        {
            targetRotation = -90f;
        }
        else if (RotateLeft.All(b => b))
        {
            targetRotation = 90f;
        }

        // Perform smooth rotation
        float elapsedTime = 0f;
        Vector3 startRotation = Rotations[0].eulerAngles;
        Vector3 targetRotationEuler = startRotation + new Vector3(0, 0, targetRotation);

        while (elapsedTime < RotateDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / RotateDuration;

            // Apply smooth rotation to all rotation pivots
            foreach (Transform rotationPivot in Rotations)
            {
                rotationPivot.rotation = Quaternion.Lerp(
                    Quaternion.Euler(startRotation),
                    Quaternion.Euler(targetRotationEuler),
                    t
                );
            }

            yield return null;
        }

        // Ensure final rotation is exact
        foreach (Transform rotationPivot in Rotations)
        {
            rotationPivot.rotation = Quaternion.Euler(targetRotationEuler);
        }

        // Clean up
        for (int i = 0; i < Levels.Count; i++)
        {
            RotateLeft[i] = false;
            RotateRight[i] = false;
            Levels[i].parent = null;
        }

        isRotating = false;
    }

    // Safety method to stop rotation if needed
    public void StopCurrentRotation()
    {
        if (currentRotationCoroutine != null)
        {
            StopCoroutine(currentRotationCoroutine);
            currentRotationCoroutine = null;
        }
        isRotating = false;
    }
}