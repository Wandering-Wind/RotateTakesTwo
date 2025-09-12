using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour //Collect coins for the first level
                                         //| Once players collet items, they can interact with a lever(or something similar, if thats what were doing) open the door
                                         //
{
    public int coinCounter;
    public TextMeshProUGUI coinCounterUI;
    public int coinMaxCount;
    public GameObject door;
    public bool isCoinMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinCounterUI.text = coinCounter.ToString() + "/5";

        if (coinCounter == coinMaxCount && !isCoinMax)
        {
            isCoinMax = true;
            Destroy(door);
        }
    }
}
