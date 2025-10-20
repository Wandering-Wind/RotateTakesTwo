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

    public List<GameObject> RotateRightUI;
    public List<GameObject> RotateLeftUI;

    public List<GameObject> RotateImageRight;
    public List<GameObject> RotateImageLeft;

    public void Rotate()
    {

        if (!isRotating)
        {
            StartCoroutine(RotateWorld());
        }

    }

    private void Update()
    {


        if (RotateRight.All(b => b))
        {
            Rotate();
        }
        else if (RotateLeft.All(b => b))
        {
            Rotate();
        }

        if (RotateRight[0])
        {
            RotateRightUI[1].SetActive(true);
        }
        else if (!RotateRight[0])
        {
            RotateRightUI[1].SetActive(false);
        }

        if (RotateRight[1])
        {
            RotateRightUI[0].SetActive(true);
        }
        else if (!RotateRight[1])
        {
            RotateRightUI[0].SetActive(false);
        }

        if (RotateLeft[0])
        {
            RotateLeftUI[1].SetActive(true);
        }
        else if (!RotateLeft[0])
        {
            RotateLeftUI[1].SetActive(false);
        }

        if (RotateLeft[1])
        {
            RotateLeftUI[0].SetActive(true);
        }
        else if (!RotateLeft[1])
        {
            RotateLeftUI[0].SetActive(false);
        }


        RotateImageRight[0].transform.Rotate(0, 0, -100 * Time.deltaTime);
        RotateImageLeft[0].transform.Rotate(0, 0, 100 * Time.deltaTime);

        RotateImageLeft[1].transform.Rotate(0, 0, -100 * Time.deltaTime);
        RotateImageRight[1].transform.Rotate(0, 0, 100 * Time.deltaTime);




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
        for (int i = 0; i < Players.Count; i++)
        {
            Rotations[i].transform.position = Players[i].transform.position;
        }
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