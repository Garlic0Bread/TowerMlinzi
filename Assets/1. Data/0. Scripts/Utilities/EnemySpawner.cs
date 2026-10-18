using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private float spawnRadius = 25f; // Distance from center
    [SerializeField] private float timeBetweenWaves = 5f;

    [Header("Tower Reference")]
    [SerializeField] private Transform towerTransform;

    void Start()
    {
        if (towerTransform == null)
            towerTransform = GameObject.FindGameObjectWithTag("Tower").transform;

        StartCoroutine(SpawnLoop());
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (towerTransform != null)
            Gizmos.DrawWireSphere(towerTransform.position, spawnRadius);
    }

    void SpawnEnemy()
    {
        // 1. Get a random angle in Radians
        float angle = Random.Range(0, Mathf.PI * 2);

        // 2. Calculate position using Cosine and Sine
        // x = cos(angle) * radius, z = sin(angle) * radius
        Vector3 spawnPos = new(Mathf.Cos(angle) * spawnRadius, 0.5f, Mathf.Sin(angle) * spawnRadius);

        // Add the Tower's position to center the circle on the site
        spawnPos += towerTransform.position;
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f); // Short warningFillSpeed between individual spawns
            }

            yield return new WaitForSeconds(timeBetweenWaves);

            enemiesPerWave++;//Make it harder as time goes on
        }
    }
}
