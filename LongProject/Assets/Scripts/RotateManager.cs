using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    public List<Transform> Levels;
    public List<Transform> Rotations;
    [SerializeField]
    private float RotateDuration;
    public List<bool> RotateRight;
    public List<bool> RotateLeft;
    [SerializeField]
    private float rotateSpeed;
    public bool isRotating;

    public List<Transform> Players;
    private bool CheckRotation;
    public void Rotate()
    {

        if (!isRotating)
        {
            StartCoroutine(RotateWorld());
        }

    }

    private void Update()
    {


        if (RotateLeft.All(b => b) || RotateRight.All(b => b))
        {
            Rotate();
        }




        if (isRotating)
        {
            if(!CheckRotation)
            {
                if (RotateLeft.All(b => b))
                {
                    foreach (Transform t in Levels)
                    {
                        t.Rotate(0, 0, 90 * Time.deltaTime);
                    }
                }
                else if (RotateRight.All(b => b))
                {
                    foreach (Transform t in Rotations)
                    {
                        t.Rotate(0, 0, -90 * Time.deltaTime);
                    }
                }
            }
        }
        else if (!isRotating)
        {
            foreach (Transform t in Levels)
            {
                if (Rotations.Count ==2 )
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
    }

    IEnumerator RotateWorld()
    {
        isRotating = true;
        CheckRotation = false;
        for (int i = 0; i < 2; i++)
        {
            Levels[i].parent = Rotations[i];
        }
        yield return new WaitForSeconds(RotateDuration); // Use your RotateDuration variable
        for (int i = 0; i < 2; i++)
        {
            RotateLeft[i] = false;
            RotateRight[i] = false;
            Levels[i].parent = null;
        }
        CheckRotation = true;
        isRotating = false;
    }
}