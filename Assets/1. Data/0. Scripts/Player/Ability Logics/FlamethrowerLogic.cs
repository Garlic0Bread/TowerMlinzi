using UnityEngine;

public class FlamethrowerLogic : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;

    public float damagePerSecond = 50f;
    [SerializeField] private float range = 5f;
    [SerializeField] private float coneAngle = 45f;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.5f);

        //left and right boundaries of the cone
        Vector3 leftRayDirection = Quaternion.AngleAxis(-coneAngle / 2, transform.up) * transform.forward;
        Vector3 rightRayDirection = Quaternion.AngleAxis(coneAngle / 2, transform.up) * transform.forward;

        Gizmos.DrawRay(transform.position, leftRayDirection * range);
        Gizmos.DrawRay(transform.position, rightRayDirection * range);

        // Draw an arc at the end of the range to visualize the "curve"
        int segments = 10;
        Vector3 previousPoint = transform.position + leftRayDirection * range;

        for (int i = 1; i <= segments; i++)
        {
            float currentAngle = -coneAngle / 2 + (coneAngle / segments) * i;
            Vector3 nextDir = Quaternion.AngleAxis(currentAngle, transform.up) * transform.forward;
            Vector3 nextPoint = transform.position + nextDir * range;

            Gizmos.DrawLine(previousPoint, nextPoint);
            previousPoint = nextPoint;
        }

        // Optional: Draw a line from origin to center-front for aiming reference
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * range);
    }

    public void UpdateBreath(float dps)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range, enemyLayer);

        foreach (var hit in hits)
        {
            Vector3 dirToEnemy = (hit.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToEnemy) < coneAngle / 2f)
            {//Check if they are within the cone angle
                Health _health = hit.GetComponent<Health>();
                Rigidbody _rb = hit.GetComponent<Rigidbody>();

                if (_health != null)
                {
                    _health.TakeDamage(dps * Time.deltaTime);
                }

                if(_rb  != null)
                {
                    Vector3 pushDir = transform.forward + Vector3.up * 0.5f;
                    _rb.AddForce(pushDir * 1.5f, ForceMode.Impulse);
                }
            }
        }
    }
}
