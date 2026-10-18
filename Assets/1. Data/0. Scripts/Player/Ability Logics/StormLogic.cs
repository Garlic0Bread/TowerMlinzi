using UnityEngine;

public class StormLogic : MonoBehaviour
{
    private Transform owner;
    private float nextStrikeTime;

    [SerializeField] private float radius = 5f;
    [SerializeField] private float damage = 40f;
    [SerializeField] private float strikeRate = 1.0f;
    [SerializeField] private GameObject lightningStrikVFX;
    [SerializeField] private LayerMask enemyLayer;

    public void Initialize(Transform followTarget, float finalRadius)
    {
        owner = followTarget;
        radius = finalRadius;
        // Scale the visual cloud to match the radius
        transform.localScale = new Vector3(radius * 2, 1, radius * 2);
    }

    void Update()
    {
        if (owner == null) return;

        transform.position = Vector3.Lerp(transform.position, owner.position, Time.deltaTime * 5f);

        if (Time.time >= nextStrikeTime)
        {
            StrikeRandomEnemy();
            nextStrikeTime = Time.time + strikeRate;
        }
    }

    void StrikeRandomEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        if (enemies.Length > 0)
        {
            // Pick a random target within the storm
            Transform target = enemies[Random.Range(0, enemies.Length)].transform;

            // Damage the target
            Health h = target.GetComponent<Health>();
            if (h != null) h.TakeDamage(damage);

            // Visual: Draw a lightning line (Debug for now)
            Instantiate(lightningStrikVFX, target.position, Quaternion.identity);

            Debug.DrawLine(transform.position + Vector3.up * 10f, target.position, Color.cyan, 0.2f);
        }
    }
}
