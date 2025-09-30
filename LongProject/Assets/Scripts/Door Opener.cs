using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField]
    private string Animation;
    [SerializeField]
    private Animator DoorAnimator;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HeavyBlock"))
        {
            DoorAnimator.SetBool(Animation, true);
        }
    }

}
