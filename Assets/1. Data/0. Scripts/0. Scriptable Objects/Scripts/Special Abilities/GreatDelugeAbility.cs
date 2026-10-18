using UnityEngine;

[CreateAssetMenu(fileName = "GreatDeluge", menuName = "Abilities/Mythic/Great Deluge")]
public class GreatDelugeAbility : AbilityData
{
    [SerializeField] private GameObject stormPrefab;
    [SerializeField] private float baseRadius = 5f;
    [SerializeField] private float duration = 12f;

    public override void Activate(GameObject parent)
    {
        PlayerController pc = parent.GetComponent<PlayerController>();

        float finalRadius = (pc.data.currentLevel >= 5) ? baseRadius * 2f : baseRadius;
        GameObject storm = Instantiate(stormPrefab, parent.transform.position, Quaternion.identity);

        StormLogic logic = storm.GetComponent<StormLogic>();
        logic.Initialize(parent.transform, finalRadius);

        if (pc.data.currentLevel >= 4)
        {
            
        }
        Destroy(storm, duration);
    }
}