using UnityEngine;

public class ExplodingEnemy : EnemyAI
{
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private float explosionDamage = 50f;
    [SerializeField] private float explosionRadius = 3f;

    protected override void OnReachTarget()
    {
        Explode();
    }
    public void Explode()
    {
        Instantiate(explosionVFX, transform.position, Quaternion.identity);

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Tower") || hit.CompareTag("Player"))
            {
                hit.GetComponent<Health>().TakeDamage(explosionDamage);
            }
        }
        Destroy(gameObject);
    }
}
