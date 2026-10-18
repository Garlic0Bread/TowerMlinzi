using UnityEngine;

public class Gatekeeper : EnemyAI
{
    public float frontalShieldReduction = 0.9f; // Blocks 90% of damage from front

    public void OnTakeDamageCustom(float incomingDamage, Vector3 attackerPos)
    {
        Vector3 dirToAttacker = (attackerPos - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToAttacker);

        if (angle < 70f) // If attacker is in a 70-degree arc in front
        {
            float reducedDamage = incomingDamage * (1f - frontalShieldReduction);
            GetComponent<Health>().TakeDamage(reducedDamage);
            Debug.Log("Blocked!");
        }
        else
        {
            GetComponent<Health>().TakeDamage(incomingDamage);
        }
    }
}
