using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject groundTile;
    private Vector3 nextSpawnPoint = Vector3.zero;

    public void SpawnTile(bool spawnItems)
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);

        // Find the spawn point child safely (or ensure index 1 is always your designated point)
        Transform nextPointTransform = temp.transform.Find("NextSpawnPoint"); // Optional safer approach if named
        if (nextPointTransform != null)
        {
            nextSpawnPoint = nextPointTransform.position;
        }
        else
        {
            // Fallback to your index method if not named
            nextSpawnPoint = temp.transform.GetChild(1).position;
        }
        if (spawnItems)
        {
            temp.GetComponent<GroundTile>().SpawnObstacle();
            temp.GetComponent<GroundTile>().SpawnCoins();
        }
    }

    private void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            if (i < 3)
            {
                SpawnTile(false);
            } else
            {
                SpawnTile(true);
            }
               
        }
    }
}