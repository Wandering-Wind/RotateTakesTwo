using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrbChecker : MonoBehaviour
{
    [SerializeField]
    private GameObject[] OrbKeyBoxes;
    [SerializeField]
    private string orbName;


    private void Start()
    {
        OrbKeyBoxes = GameObject.FindGameObjectsWithTag("KeyBox");
    }

    private void Update()
    {
       if (transform.childCount >0)
        {
            GameObject childObject = transform.GetChild(0).gameObject;
            if (childObject.name != orbName)
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
            }
        }
    }
}
