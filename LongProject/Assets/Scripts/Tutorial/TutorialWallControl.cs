using System.Collections;
using UnityEngine;

public class TutorialWallControl : MonoBehaviour
{
    [Header("Wall Movement Settings")]
    [SerializeField] private Transform wallTransform;
    [SerializeField] private float moveDistanceDown = 5f; //How far down wall moves
    [SerializeField] private float moveSpeed = 2f; // Movement speed

    private bool wallMoved = false;

    // Call this from the scanner
    public void MoveWallDown()
    {
        if (wallMoved || wallTransform == null) return;

        StartCoroutine(MoveWallCoroutine());
        wallMoved = true;
    }

    private IEnumerator MoveWallCoroutine()
    {
        Vector3 startPos = wallTransform.position;
        Vector3 targetPos = startPos - new Vector3(0, moveDistanceDown, 0); // Move downward

        while (Vector3.Distance(wallTransform.position, targetPos) > 0.01f)
        {
            wallTransform.position = Vector3.MoveTowards(wallTransform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
