using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class HandScanManager : MonoBehaviour
{
    public MeshRenderer HandScannerMesh, HandScannerMesh2;
    [SerializeField]
    private float currentTime = 0f;
    private int Seconds = 0;
    Material ScannerMaterial;
    Material Scanner2Material;
    [SerializeField]
    private int WaitTime;
    [SerializeField]
    private TextMeshProUGUI TimerText;
    public GameObject TimerBG;
    private bool TimerActivated;
    public Animator FloorMover;

    private void Start()
    {
        Material[] allMaterials = HandScannerMesh.materials;
        ScannerMaterial = HandScannerMesh.materials[1];

        Material[] allMaterials2 = HandScannerMesh2.materials;
        Scanner2Material = HandScannerMesh2.materials[1];
    }

   

    private void Update()
    {
        if (ScannerMaterial.color == Color.green && Scanner2Material.color != Color.green)
        {
            if (!TimerActivated)
            {
                currentTime = WaitTime;
                TimerActivated = true;
            }
            TimerText.text = Seconds.ToString();
        }
        else if (ScannerMaterial.color != Color.green && Scanner2Material.color == Color.green)
        {
            if (!TimerActivated)
            {
                currentTime = WaitTime;
                TimerActivated = true;
            }
            TimerText.text = Seconds.ToString();
        }
        else if (ScannerMaterial.color == Color.green && Scanner2Material.color == Color.green)
        {
            
            TimerText.text = "";
        }
        else if (ScannerMaterial.color != Color.green && Scanner2Material.color != Color.green)
        {

            TimerText.text = "";
        }

        if (Seconds <= 0)
        {
            if (Scanner2Material.color != Color.green && ScannerMaterial.color != Color.green)
            {
                ScannerMaterial.color = Color.red;
                Scanner2Material.color = Color.red;
            }
            else if (Scanner2Material.color == Color.green && ScannerMaterial.color == Color.green)
            {
                ScannerMaterial.color = Color.green;
                Scanner2Material.color = Color.green;
                FloorMover.SetBool("Move", true);
            }
            TimerBG.SetActive(false);
            TimerActivated = false;

        }
        else if (Seconds > 0)
        {
            currentTime -= Time.deltaTime;
            TimerBG.SetActive(true);
        }

        Seconds = Mathf.FloorToInt(currentTime);
    }
}
