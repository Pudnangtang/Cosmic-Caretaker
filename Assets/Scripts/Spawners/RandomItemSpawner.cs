using UnityEngine;

public class RandomItemSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // Array to hold your prefabs
    public float spawnInterval = 2f; // Time interval between spawns
    public Vector2 spawnZoneMin; // Minimum corner of the spawn zone
    public Vector2 spawnZoneMax; // Maximum corner of the spawn zone

    void Start()
    {
        InvokeRepeating("SpawnRandomPrefab", 0f, spawnInterval);
    }

    void SpawnRandomPrefab()
    {
        // Generate a random position within the spawn zone
        float randomX = Random.Range(spawnZoneMin.x, spawnZoneMax.x);
        float randomY = Random.Range(spawnZoneMin.y, spawnZoneMax.y);
        Vector3 randomPosition = new Vector3(randomX, randomY, 0);

        // Choose a random prefab
        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];

        // Instantiate the prefab
        GameObject spawnedObject = Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);

        // Destroy the spawned object after 10 seconds
        Destroy(spawnedObject, 10f);
    }

}
