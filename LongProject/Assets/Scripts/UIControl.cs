using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public GameObject PausePanel;
    public List<GameObject> KeysCollectedUI;

    public void PauseandPlay()
    {
        if (PausePanel.activeSelf)
        {
            PausePanel.SetActive(false);
        }
        else if(!PausePanel.activeSelf)
        {
            PausePanel.SetActive(true);
        }
    }

    public void NexScene(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("Level1");
    }

}
