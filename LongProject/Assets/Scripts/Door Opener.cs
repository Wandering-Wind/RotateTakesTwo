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
    [SerializeField]
    private bool isDoorOpenerButton;
    [SerializeField]
    private Animator LeftDoor, RightDoor;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HeavyBlock"))
        {
            DoorAnimator.SetBool(Animation, true);
            collision.gameObject.transform.position = transform.position;
            collision.gameObject.transform.parent = transform;
        }
    }

    public void OpenDoors()
    {
        LeftDoor.SetBool("Open", true);
        RightDoor.SetBool("Open", true);
    }

}
