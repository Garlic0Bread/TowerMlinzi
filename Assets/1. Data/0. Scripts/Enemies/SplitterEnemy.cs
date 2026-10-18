using UnityEngine;

public class SplitterEnemy : EnemyAI
{
    public GameObject sprinterPrefab;
    public int splitCount = 4;

    public void OnDeathSplit()
    {
        for (int i = 0; i < splitCount; i++)
        {
            Vector3 randomOffset = new (Random.Range(-1, 1), 0, Random.Range(-1, 1));
            Instantiate(sprinterPrefab, transform.position + randomOffset, Quaternion.identity);
        }
    }
}
