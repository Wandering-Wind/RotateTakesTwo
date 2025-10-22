using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    public List<bool> KeysCollected;
    public bool canOpenDoors;
    public GameObject WinScreen;
    public List<bool> DoorsOpened;
    private void Update()
    {
        if (KeysCollected.All(b => b))
        {
            canOpenDoors = true;
        }

        if (DoorsOpened.All(b => b))
        {
            WinScreen.SetActive(true);
        }
       
    }
}
