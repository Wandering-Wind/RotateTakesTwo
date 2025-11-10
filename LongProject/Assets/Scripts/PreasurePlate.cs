using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreasurePlate : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player2"))
        {
            PlayerController playerControllerscript = collision.gameObject.GetComponent<PlayerController>();
            if (playerControllerscript != null )
            {
                if (playerControllerscript.canSmash)
                {
                    print("smashed");
                }
            }
        }
    }
}
