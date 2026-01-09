using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject obstaclePrefab;
    public float baseMinInterval = 2f;
    public float baseMaxInterval = 4f;

    [Header("Spawn Position")]
    public Transform spawnPoint;

    [Header("Side Identification")]
    public bool isLeftSide = true; // True for blue/left, false for red/right

    [Header("Difficulty Scaling")]
    public float intervalDecreaseRate = 0.1f;
    public float minInterval = 0.5f;

    private float nextSpawnTime;
    private static int lastSpawnedSide = -1; // -1 = none, 0 = left, 1 = right
    private static float lastGlobalSpawnTime = -999f;

    void Start()
    {
        nextSpawnTime = Time.time + Random.Range(1f, 3f);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            int mySide = isLeftSide ? 0 : 1;

            // Check if it's this spawner's turn to spawn
            if (lastSpawnedSide != mySide)
            {
                SpawnObstacle();
                lastSpawnedSide = mySide;
                lastGlobalSpawnTime = Time.time;

                // Calculate current intervals based on game time
                float currentMinInterval = Mathf.Max(minInterval, baseMinInterval - (GameManager.Instance.GetElapsedTime() / 30f) * intervalDecreaseRate * baseMinInterval);
                float currentMaxInterval = Mathf.Max(minInterval + 0.5f, baseMaxInterval - (GameManager.Instance.GetElapsedTime() / 30f) * intervalDecreaseRate * baseMaxInterval);

                nextSpawnTime = Time.time + Random.Range(currentMinInterval, currentMaxInterval);
            }
            else
            {
                // Not our turn yet, try again soon
                nextSpawnTime = Time.time + 0.1f;
            }
        }
    }

    void SpawnObstacle()
    {
        if (obstaclePrefab != null && spawnPoint != null)
        {
            Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
