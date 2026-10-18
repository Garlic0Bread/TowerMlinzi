using UnityEngine;

public class LootDropper : MonoBehaviour
{
    [SerializeField] private int amount = 5;
    [SerializeField] private GameObject cowriePrefab;

    public void DropLoot()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 randomOffset = new (Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            Instantiate(cowriePrefab, transform.position + randomOffset, Quaternion.identity);
        }
    }
}
