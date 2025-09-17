using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    public Transform[] Levels;
    [SerializeField]
    private float RotateDuration;
    private List<bool> RotateRight;
    private List<bool> RotateLeft;
    [SerializeField]
    private float rotateSpeed;
    private bool isRotating;

    public void Rotate()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateWorld());
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Rotate();
        }

        if (isRotating)
        {
            foreach (Transform t in Levels)
            {
                t.Rotate(0, 0, 90 * Time.deltaTime);
            }
        }
        else if (!isRotating)
        {
            foreach (Transform t in Levels)
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
        yield return new WaitForSeconds(RotateDuration); // Use your RotateDuration variable
        isRotating = false;
    }
}