using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwapManager : MonoBehaviour
{
    public List<Transform> Players;
    public List<bool> CanSwap;
    public bool isSwapping;
    [SerializeField]
    private List<Vector3> PlayerPositions;
    [SerializeField]
    private RotateManager RotateManagerScript;
    public List<GameObject> SwapUI;

    private void Update()
    {
        if (CanSwap.All(b => b))
        {
            if(!isSwapping)
            {
                Swap();
            }

        }

        if (CanSwap[0])
        {
            SwapUI[1].SetActive(true);
        }
        else if (!CanSwap[0])
        {
            SwapUI[1].SetActive(false);
        }

        if (CanSwap[1])
        {
            SwapUI[0].SetActive(true);
        }
        else if (!CanSwap[1])
        {
            SwapUI[0].SetActive(false);
        }
    }
    public void Swap()
    {
        if (Players.Count == 2)
        {
            isSwapping = true;

            // Save player 0's position
            Vector3 temp = Players[0].position;

            // Swap directly
            Players[0].position = Players[1].position;
            Players[1].position = temp;

            // Reset swap states
            for (int i = 0; i < Players.Count; i++)
            {
                CanSwap[i] = false;
            }
            RotateManagerScript.Levels.Reverse();

            isSwapping = false;
        }
    }



}
