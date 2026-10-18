using UnityEngine;

public class SummonAI : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float attackRate = 1.2f;
    [SerializeField] private float attackDamage = 15f;
    [SerializeField] private float detectionRange = 8f;
    [SerializeField] private float followDistance = 3f;

    private Transform owner;
    private float nextAttackTime;
    private Transform currentTarget;

    public void Initialize(Transform creator)
    {
        owner = creator;
    }

    void Update()
    {
        if (owner == null) return;

        FindTarget();

        if (currentTarget != null)
        {
            MoveToward(currentTarget.position);
            TryAttack();
        }
        else
        {
            // If no enemies, return to Madabula
            if (Vector3.Distance(transform.position, owner.position) > followDistance)
            {
                MoveToward(owner.position);
            }
        }
    }

    void TryAttack()
    {
        if (Time.time >= nextAttackTime && Vector3.Distance(transform.position, currentTarget.position) < 2f)
        {
            Health h = currentTarget.GetComponent<Health>();
            if (h != null)
            {
                h.TakeDamage(attackDamage);
                GameObject.FindWithTag("Tower").GetComponent<Health>().Heal(attackDamage * 0.05f);
            }
            nextAttackTime = Time.time + (1f / attackRate);
        }
    }
    void FindTarget()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, detectionRange);
        float closestDist = Mathf.Infinity;

        foreach (var e in enemies)
        {
            if (e.CompareTag("Enemy"))
            {
                float d = Vector3.Distance(transform.position, e.transform.position);
                if (d < closestDist)
                {
                    closestDist = d;
                    currentTarget = e.transform;
                }
            }
        }
    }
    void MoveToward(Vector3 pos)
    {
        Vector3 dir = (pos - transform.position).normalized;
        dir.y = 0;
        transform.position += dir * moveSpeed * Time.deltaTime;

        // Flip sprite to face direction
        if (dir.x != 0)
            transform.localScale = new Vector3(dir.x > 0 ? 1 : -1, 1, 1);
    }
}
