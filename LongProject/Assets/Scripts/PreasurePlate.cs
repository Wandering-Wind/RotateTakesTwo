using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreasurePlate : MonoBehaviour
{
    [SerializeField]
    private bool isButton, isPresurePlate;
    private Animator buttonPress;
    private ButtonKeySpawner buttonKeySpawnerScript;
    BoxCollider bc;

    private void Start()
    {
        if (isButton)
        {
            buttonPress = GetComponent<Animator>();
            buttonKeySpawnerScript = GetComponent<ButtonKeySpawner>();
            bc = GetComponent<BoxCollider>();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player2"))
        {
            PlayerController playerControllerscript = collision.gameObject.GetComponent<PlayerController>();
            if (playerControllerscript != null && playerControllerscript.canSmash)
            {
                if (isButton)
                {
                    buttonKeySpawnerScript.SpawnKey();
                    StartCoroutine(buttonPressAnimation());
                }
            }
        }
    }

    IEnumerator buttonPressAnimation()
    {
        bc.isTrigger = true;
        buttonPress.SetBool("Press", true);
        yield return new WaitForSeconds(1);
        buttonPress.SetBool("Press", false);
        bc.isTrigger = false;

    }
}
