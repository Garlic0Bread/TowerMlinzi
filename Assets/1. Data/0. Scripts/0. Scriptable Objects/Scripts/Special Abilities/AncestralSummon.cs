using UnityEngine;

[CreateAssetMenu(fileName = "AncestralAlly", menuName = "Abilities/Mythic/Ancestral Ally")]
public class AncestralSummon : AbilityData
{
    public GameObject allyPrefab;
    public float baseDuration = 10f;

    public override void Activate(GameObject parent)
    {
        PlayerController pc = parent.GetComponent<PlayerController>();

        // Level 2 Benefit: Ally lasts longer
        float finalDuration = baseDuration;
        if (pc.data.currentLevel >= 2) finalDuration += 10f;

        // Spawn near the player
        Vector3 spawnPos = parent.transform.position + parent.transform.right * 2f;
        GameObject ally = Instantiate(allyPrefab, spawnPos, Quaternion.identity);

        // Setup AI
        SummonAI ai = ally.GetComponent<SummonAI>();
        ai.Initialize(parent.transform);

        // Level 4 Benefit: Twin Souls (Summon a second one)
        if (pc.data.currentLevel >= 4)
        {
            Vector3 spawnPos2 = parent.transform.position - parent.transform.right * 2f;
            GameObject ally2 = Instantiate(allyPrefab, spawnPos2, Quaternion.identity);
            ally2.GetComponent<SummonAI>().Initialize(parent.transform);
            Destroy(ally2, finalDuration);
        }

        Destroy(ally, finalDuration);
        Debug.Log($"Madabula summoned an Ancestral Ally for {finalDuration}s!");
    }
}
