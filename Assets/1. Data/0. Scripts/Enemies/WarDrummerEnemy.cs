using UnityEngine;

public class WarDrummer : EnemyAI
{
    [SerializeField] private float speedBuff = 1.2f;

    void FixedUpdate()
    {
        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, attackRange);
        foreach (var col in nearbyEnemies)
        {
            if (col.CompareTag("Enemy") && col.gameObject != this.gameObject)
            {
                EnemyAI otherEnemy = col.GetComponent<EnemyAI>();
                if (otherEnemy != null)
                {
                    otherEnemy.CurrentMoveSpeed = otherEnemy.template.moveSpeed * speedBuff;
                }
            }
        }
    }
}
