using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "MansasRain", menuName = "Abilities/Mandinka/Mansa's Rain")]
public class MansasRainAbility : AbilityData
{
    [SerializeField] private GameObject warningCirclePrefab;
    [SerializeField] private float warningFillSpeed = 1.5f;
    [SerializeField] private float damageDelay = 1.5f;
    [SerializeField] private float totalDamage = 100f;
    [SerializeField] private GameObject arrowRainVFX;
    [SerializeField] private float radius = 8f;

    public override void Activate(GameObject parent)
    {
        Vector3 spawnPos = parent.transform.position;
        spawnPos.y = 0.05f; // Slightly above ground

        GameObject circle = Instantiate(warningCirclePrefab, spawnPos, Quaternion.Euler(90, 0, 0));
        circle.GetComponent<WarningCircle>().Setup(radius, warningFillSpeed);

        parent.GetComponent<PlayerController>().StartCoroutine(DropArrows(spawnPos));
    }

    IEnumerator DropArrows(Vector3 center)
    {
        yield return new WaitForSeconds(warningFillSpeed);

        Instantiate(arrowRainVFX, center + Vector3.up * 10f, Quaternion.identity);

        yield return new WaitForSeconds(damageDelay);

        Collider[] enemies = Physics.OverlapSphere(center, radius);
        foreach (var col in enemies)
        {
            if (col.CompareTag("Enemy"))
            {
                Health h = col.GetComponent<Health>();
                if (h != null) h.TakeDamage(totalDamage);
            }
        }
    }
}