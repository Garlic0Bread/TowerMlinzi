using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "DragonsBreath", menuName = "Abilities/Mythic/Dragon's Breath")]
public class DragonsBreathAbility : AbilityData
{
    public float duration = 4f;
    public GameObject fireVFXPrefab;

    public override void Activate(GameObject parent)
    {
        // Start a Coroutine on the parent to handle the duration
        parent.GetComponent<PlayerController>().StartCoroutine(ExecuteBreath(parent));
    }

    IEnumerator ExecuteBreath(GameObject parent)
    {
        Transform fPoint = parent.transform.Find("FirePoint");

        GameObject fx = Instantiate(fireVFXPrefab, fPoint);
        fx.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        FlamethrowerLogic logic = fx.GetComponent<FlamethrowerLogic>();
        var baseDPS = logic.damagePerSecond;

        float elapsed = 0;
        while (elapsed < duration)
        {
            if (logic != null) logic.UpdateBreath(baseDPS);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(fx);
    }
}
