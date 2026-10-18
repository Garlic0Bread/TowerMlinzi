using System.Collections.Generic;
using UnityEngine;

public class WaveDirector : MonoBehaviour
{
    private float matchTimer = 0f;
    private Dictionary<int, float> lastSpawnTimes = new ();

    [Header("Match Settings")]
    [SerializeField] private float matchDuration = 600f;
    [SerializeField] private float spawnRadius = 25f; // Distance from center

    [Header("Wave Schedule")]
    public List<SpawnConfig> waves;
    [SerializeField] private Transform towerTransform;

    private void Start()
    {
        if (towerTransform == null)
            towerTransform = GameObject.FindGameObjectWithTag("Tower").transform;
    }
    private void Update()
    {
        if (matchTimer >= matchDuration)
        {
            EndMatch();
            return;
        }

        matchTimer += Time.deltaTime;
        CheckWaves();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (towerTransform != null)
            Gizmos.DrawWireSphere(towerTransform.position, spawnRadius);
    }

    void EndMatch()
    {
        Debug.Log("Time is up! The Site is Restored!");
        // Trigger Win UI here
    }
    void CheckWaves()
    {
        for (int i = 0; i < waves.Count; i++)
        {
            SpawnConfig config = waves[i];

            // Check if we are currently in this wave's time window
            if (matchTimer >= config.startTime && matchTimer <= config.endTime)
            {
                // Initialize the timer tracking for this config if not present
                if (!lastSpawnTimes.ContainsKey(i))
                    lastSpawnTimes[i] = matchTimer;

                // Check if it's time to spawn the next batch
                if (matchTimer >= lastSpawnTimes[i] + config.interval)
                {
                    SpawnBatch(config);
                    lastSpawnTimes[i] = matchTimer;
                }
            }
        }
    }
    void SpawnBatch(SpawnConfig config)
    {
        for (int i = 0; i < config.amountPerInterval; i++)
        {
            // 1. Get a random angle in Radians
            float angle = Random.Range(0, Mathf.PI * 2);
            Vector3 spawnPos = new(Mathf.Cos(angle) * spawnRadius, 0.5f, Mathf.Sin(angle) * spawnRadius);
            spawnPos += towerTransform.position;

            GameObject enemy = Instantiate(config.enemyPrefab, spawnPos, Quaternion.identity);

            EnemyAI ai = enemy.GetComponent<EnemyAI>();
            if (ai != null)
            {
                ai.template = config.enemyTemplate;
            }
        }
    }
}

[System.Serializable]
public struct SpawnConfig
{
    public EnemyTemplate enemyTemplate;
    public GameObject enemyPrefab; // The visual/logic prefab for this enemy
    public int amountPerInterval;
    public float interval; // Seconds between spawns
    public float startTime; // When in the match this starts (in seconds)
    public float endTime;   // When this stops spawning
}