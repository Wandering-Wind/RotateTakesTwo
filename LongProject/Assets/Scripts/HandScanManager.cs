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
        // Get materials safely
        if (HandScannerMesh != null && HandScannerMesh.materials.Length > 1)
            ScannerMaterial = HandScannerMesh.materials[1];

        if (HandScannerMesh2 != null && HandScannerMesh2.materials.Length > 1)
            Scanner2Material = HandScannerMesh2.materials[1];
    }

    private void Update()
    {
        UpdateTimerLogic();
        HandleTimerCountdown();
        UpdateTimerDisplay();
    }

    private void UpdateTimerLogic()
    {
        bool scanner1Green = IsMaterialColor(ScannerMaterial, Color.green);
        bool scanner2Green = IsMaterialColor(Scanner2Material, Color.green);

        // Start timer if only one scanner is green
        if ((scanner1Green && !scanner2Green) || (!scanner1Green && scanner2Green))
        {
            if (!TimerActivated)
            {
                currentTime = WaitTime;
                TimerActivated = true;
            }
        }
        // Both green or both not green - clear timer text
        else
        {
            TimerText.text = "";
        }
    }

    private void HandleTimerCountdown()
    {
        if (TimerActivated)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                TimerBG.SetActive(true);
            }
            else
            {
                HandleTimerExpired();
            }
        }
    }

    private void HandleTimerExpired()
    {
        bool scanner1Green = IsMaterialColor(ScannerMaterial, Color.green);
        bool scanner2Green = IsMaterialColor(Scanner2Material, Color.green);

        // Scanner 1
        if (scanner1Green)
        {
            ScannerMaterial.EnableKeyword("_EMISSION");
            ScannerMaterial.SetColor("_EmissionColor", Color.green * 2.0f);
            ScannerMaterial.color = Color.green; // Also change base color
        }
        else
        {
            ScannerMaterial.EnableKeyword("_EMISSION"); // Enable for red too!
            ScannerMaterial.SetColor("_EmissionColor", Color.red * 2.0f);
            ScannerMaterial.color = Color.red; // Also change base color
        }

        // Scanner 2
        if (scanner2Green)
        {
            Scanner2Material.EnableKeyword("_EMISSION");
            Scanner2Material.SetColor("_EmissionColor", Color.green * 2.0f);
            Scanner2Material.color = Color.green; // Also change base color
        }
        else
        {
            Scanner2Material.EnableKeyword("_EMISSION"); // Enable for red too!
            Scanner2Material.SetColor("_EmissionColor", Color.red * 2.0f);
            Scanner2Material.color = Color.red; // Also change base color
        }
        // If timer expires and scanners aren't both green, reset to red
        if (!scanner1Green || !scanner2Green)
        {
            SetScannerColor(ScannerMaterial, Color.red);
            SetScannerColor(Scanner2Material, Color.red);
        }

        TimerBG.SetActive(false);
        TimerActivated = false;
    }

    private void UpdateTimerDisplay()
    {
        Seconds = Mathf.FloorToInt(currentTime);

        // Only update text if timer is active and time remains
        if (TimerActivated && currentTime > 0)
        {
            TimerText.text = Seconds.ToString();
        }

        // Check if both scanners are green to activate floor mover
        if (IsMaterialColor(ScannerMaterial, Color.green) && IsMaterialColor(Scanner2Material, Color.green))
        {
            FloorMover.SetBool("Move", true);
        }
    }

    // Helper method to safely check material color
    private bool IsMaterialColor(Material material, Color color)
    {
        return material != null && material.color == color;
    }

    // Helper method to safely set material color
    private void SetScannerColor(Material material, Color color)
    {
        if (material != null)
        {
            material.color = color;
        }
    }
}