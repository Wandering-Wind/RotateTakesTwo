using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public List<bool> KeysCollected;
    public bool canOpenDoors;
    public GameObject WinScreen;
    public List<bool> DoorsOpened;
    [SerializeField]
    private string Scene;
    private void Update()
    {
        if (KeysCollected.All(b => b))
        {
            canOpenDoors = true;
        }

        if (DoorsOpened.All(b => b))
        {
            SceneManager.LoadScene(Scene);
        }
       
    }
}
