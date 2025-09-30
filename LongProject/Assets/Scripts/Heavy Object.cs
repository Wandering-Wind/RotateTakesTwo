using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyObject : MonoBehaviour
{
    private Transform Parent;

    private void Start()
    {
        Parent = transform.parent;
    }

    private void Update()
    {
        if (transform.parent == null)
        {
            transform.parent = Parent;
        }
    }

}
