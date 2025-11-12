using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HandScannerTutorial : MonoBehaviour
{
    [Header("Scanner Setup")]
    public MeshRenderer handScannerMesh;
    private Material scannerMaterial;

    [Header("Scanner Event")]
    public UnityEvent OnScannerActivated;

    private bool scannerTriggered = false;

    private void Start()
    {
        if (handScannerMesh != null && handScannerMesh.materials.Length > 1)
            scannerMaterial = handScannerMesh.materials[1];
    }

    private void Update()
    {
        if (scannerMaterial != null && !scannerTriggered)
        {
            // Check if the scanner is green
            if (scannerMaterial.color == Color.green)
            {
                scannerTriggered = true;
                OnScannerActivated?.Invoke();
            }
        }
    }
}
