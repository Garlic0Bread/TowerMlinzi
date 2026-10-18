using UnityEngine;

[CreateAssetMenu(fileName = "MonolithCall", menuName = "Abilities/Nyatsimba/Monolith Call")]
public class MonolithCall : AbilityData
{
    public GameObject pillarPrefab;
    public float triangleRadius = 4f; 

    public override void Activate(GameObject parent)
    {
        GameObject tower = GameObject.FindGameObjectWithTag("Tower");
        if (tower == null) return;

        Vector3 center = tower.transform.position;

        // Calculate 3 points in a triangle (0, 120, and 240 degrees)
        int pillarCount = (parent.GetComponent<PlayerController>().data.currentLevel >= 5) ? 6 : 3;
        for (int i = 0; i < pillarCount; i++)
        {
            float angle = i * (360f / pillarCount) * Mathf.Deg2Rad;
            Vector3 spawnOffset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * triangleRadius;
            Vector3 spawnPos = center + spawnOffset;

            GameObject pillar = Instantiate(pillarPrefab, spawnPos, Quaternion.identity);

            // Pop up effect
            pillar.transform.position -= Vector3.up * 2f;
            LeanTween.moveY(pillar, center.y, 1.5f).setEaseOutBack(); 
        }
    }
}
