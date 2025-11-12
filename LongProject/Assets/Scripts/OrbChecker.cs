using UnityEngine;

public class OrbChecker : MonoBehaviour
{
    [SerializeField] private GameObject[] OrbKeyBoxes;
    [SerializeField] private string orbName;
    [SerializeField] private Animator LeftDoor, RightDoor, LeftDoor2, RightDoor2;
    private void Start()
    {
        // Cache all key boxes tagged "KeyBox"
        OrbKeyBoxes = GameObject.FindGameObjectsWithTag("KeyBox");
    }

    private void Update()
    {
        // Only run check if this object has an orb child
        if (transform.childCount > 0)
        {
            GameObject childObject = transform.GetChild(0).gameObject;

            // If the orb placed here is NOT the correct one — reset all boxes
            if (childObject.name != orbName)
            {
                ResetAllBoxes();
                return;
            }

            // Otherwise, check if every box is correctly filled
            CheckAllBoxes();
        }
    }

    private void ResetAllBoxes()
    {
        for (int i = 0; i < OrbKeyBoxes.Length; i++)
        {
            if (OrbKeyBoxes[i].transform.childCount > 0)
            {
                GameObject otherChild = OrbKeyBoxes[i].transform.GetChild(0).gameObject;
                if (otherChild != null)
                {
                    Destroy(otherChild);
                }
            }
        }

        Debug.Log(" Incorrect orb placed — puzzle reset!");
    }

    private void CheckAllBoxes()
    {
        bool allCorrect = true;

        foreach (GameObject box in OrbKeyBoxes)
        {
            // Each KeyBox should have its own OrbChecker script
            OrbChecker checker = box.GetComponent<OrbChecker>();
            if (checker == null)
                continue;

            // Check if the box has an orb child
            if (box.transform.childCount == 0)
            {
                allCorrect = false;
                break;
            }

            GameObject child = box.transform.GetChild(0).gameObject;

            // If the orb name doesn't match expected name, fail the check
            if (child.name != checker.orbName)
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {

            LeftDoor.SetBool("Open", true);
            RightDoor.SetBool("Open", true);
            LeftDoor2.SetBool("Open", true);
            RightDoor2.SetBool("Open", true) ;
        }
    }
}
