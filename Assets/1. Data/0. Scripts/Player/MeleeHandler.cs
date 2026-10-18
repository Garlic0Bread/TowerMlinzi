using UnityEngine;

public class MeleeHandler : MonoBehaviour
{
    public void PerformMeleeAttack(Transform origin, float range, float damage, MeleeStats stats, LayerMask enemyLayer)
    {
        Collider[] potentialTargets = Physics.OverlapSphere(origin.position, range, enemyLayer);

        GameObject slashVFX = stats.swingEffect;
        Instantiate(slashVFX, origin.position, stats.swingEffect.transform.rotation);

        foreach (Collider col in potentialTargets)
        {
            Vector3 dirToEnemy = (col.transform.position - origin.position).normalized;
            float angleToEnemy = Vector3.Angle(origin.forward, dirToEnemy);

            if (angleToEnemy <= stats.swingArc / 2)
            {
                Health enemyHealth = col.GetComponent<Health>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);

                    ApplyKnockback(col.gameObject, dirToEnemy, stats.knockbackForce);
                }
            }
        }
    }

    private void ApplyKnockback(GameObject target, Vector3 direction, float force)
    {
        Rigidbody targetRb = target.GetComponent<Rigidbody>();
        if (targetRb != null)
        {
            direction.y = 0;
            targetRb.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}
