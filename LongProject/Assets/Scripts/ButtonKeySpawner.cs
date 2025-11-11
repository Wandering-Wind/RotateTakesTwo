using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonKeySpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> keySpawnPoints;
    [SerializeField]
    private GameObject KeyPrefab;

    public void SpawnKey()
    {
        if (KeyPrefab != null)
        {
            GameObject Key = Instantiate(KeyPrefab, keySpawnPoints[Random.Range(0, keySpawnPoints.Count)].position, Quaternion.identity);
            Destroy(Key, 10);
        }
    }
}
