using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorOpener : MonoBehaviour
{
    public bool RightSide, LeftSide;
    public GameObject OtherSide;
    [SerializeField]
    private Animator LeftDoor, RightDoor;
    [SerializeField]
    private bool isLeftSide, isRightSide;
    private DoubleDoorOpener doubleDooropenerScript;
    private Animator buttonAnimator;

    private void Start()
    {
        if (OtherSide != null)
        {
            doubleDooropenerScript = OtherSide.GetComponent<DoubleDoorOpener>();
        }
        buttonAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (RightSide && LeftSide)
        {
            OpenDoors();
        }
    }
    public void OpenDoors()
    {
        LeftDoor.SetBool("Open", true);
        RightDoor.SetBool("Open", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isRightSide)
        {
            if (collision.collider.CompareTag("Player2"))
            {
                PlayerController playerControllerscript = collision.gameObject.GetComponent<PlayerController>();
                if (playerControllerscript != null && playerControllerscript.canSmash)
                {
                    if (doubleDooropenerScript != null)
                    {
                        doubleDooropenerScript.RightSide = true;
                    }
                    RightSide = true;
                    buttonAnimator.SetBool("Press", true);
                }
            }
        }
        else if (isLeftSide)
        {
            if (collision.collider.CompareTag("HeavyBlock"))
            {
                LeftSide = true;
                doubleDooropenerScript.LeftSide = true;
            }
        }
    }
}
