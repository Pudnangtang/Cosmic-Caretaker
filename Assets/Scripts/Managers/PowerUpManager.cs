using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; // Array to hold different power-up prefabs
    public float spawnInterval = 30f; // Interval in seconds between power-up spawns
    public Vector2 spawnZoneMin; // Minimum corner of the spawn zone
    public Vector2 spawnZoneMax; // Maximum corner of the spawn zone

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnRandomPowerUp();
            timer = spawnInterval; // Reset the timer
        }
    }

    void SpawnRandomPowerUp()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnZoneMin.x, spawnZoneMax.x),
            Random.Range(spawnZoneMin.y, spawnZoneMax.y),
            0); // Assuming a 2D game

        // Randomly select a power-up prefab to spawn
        int randomIndex = Random.Range(0, powerUpPrefabs.Length);
        GameObject prefabToSpawn = powerUpPrefabs[randomIndex];

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}
