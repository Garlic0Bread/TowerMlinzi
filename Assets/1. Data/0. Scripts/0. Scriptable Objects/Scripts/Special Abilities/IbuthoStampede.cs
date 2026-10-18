using UnityEngine;

[CreateAssetMenu(fileName = "IbuthoStampede", menuName = "Abilities/Zulu/Ibutho Stampede")]
public class IbuthoStampede : AbilityData
{
    public GameObject warriorPrefab;
    public float warriorSpacing = 1.5f;

    public override void Activate(GameObject parent)
    {
        PlayerController pc = parent.GetComponent<PlayerController>();
        int warriorCount = 4 + (pc.data.currentLevel - 1);//increase by 1 for every level after level 1

        Vector3 forwardDir = parent.transform.forward;
        Vector3 rightDir = parent.transform.right; // To spread them sideways

        // 3. Calculate total width to center the line
        float totalWidth = (warriorCount - 1) * warriorSpacing;
        Vector3 startPoint = parent.transform.position - (rightDir * (totalWidth / 2f));

        for (int i = 0; i < warriorCount; i++)
        {
            Vector3 spawnPos = startPoint + (rightDir * i * warriorSpacing);
            spawnPos += forwardDir * 1.5f;// Spawn slightly in front of player

            GameObject warrior = Instantiate(warriorPrefab, spawnPos, Quaternion.LookRotation(forwardDir));
        }
    }
}