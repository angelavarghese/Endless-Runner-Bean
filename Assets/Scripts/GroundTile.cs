using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    private GroundSpawner groundSpawner;
    private Transform playerTransform;
    private int obstacleSpawnIndex;
    private bool spawnedNext = false;

    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject coinPrefab;

    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null || spawnedNext) return;

        // Spawn next tile and destroy this one once the player passes it
        if (playerTransform.position.z > transform.position.z + 5f)
        {
            if (groundSpawner != null)
            {
                groundSpawner.SpawnTile(true);
            }
            spawnedNext = true;
            Destroy(gameObject, 2f);
        }
    }

    private Vector3 obstaclePosition; // Store obstacle position to check against

    public void SpawnObstacle()
    {
        if (obstaclePrefab == null) return;

        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex);

        obstaclePosition = spawnPoint.position; // Save it here
        Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
    }

    //void SpawnCoins()
    //{
    //    if (coinPrefab == null) return;
    //    int coinsToSpawn = 10;
    //    for (int i = 0; i < coinsToSpawn; i++)
    //    {
    //        GameObject temp = Instantiate(coinPrefab);
    //        temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
    //    }
    //}

    public void SpawnCoins()
    {
        if (coinPrefab == null) return;
        int coinsToSpawn = 10;

        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);
            Vector3 coinPos = GetRandomPointInCollider(GetComponent<Collider>());

            // If the random coin point is too close to the obstacle, push it forward along Z
            if (Mathf.Abs(coinPos.x - obstaclePosition.x) < 1.5f && Mathf.Abs(coinPos.z - obstaclePosition.z) < 2.5f)
            {
                coinPos.z = obstaclePosition.z + 3f;
            }

            temp.transform.position = coinPos;
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider)
    {
        // Use the mesh filter/renderer size or fallback to local offset ranges
        Vector3 point = new Vector3(
            Random.Range(-4f, 4f), // Spread across the width of the tile
            1f,                    // Height
            Random.Range(-4f, 4f)  // Spread along the length of the tile
        );

        // Convert local position to world position based on the tile
        return transform.TransformPoint(point);
    }
}