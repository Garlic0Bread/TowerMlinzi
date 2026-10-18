using UnityEngine;

public class SpiritSiphon : EnemyAI
{
    public LineRenderer beamLine;
    public float drainRate = 5f;

    protected override void OnReachTarget()
    {
        rb.linearVelocity = Vector3.zero;
        beamLine.enabled = true;
        beamLine.SetPosition(0, transform.position + Vector3.up);
        beamLine.SetPosition(1, target.position);

        Health towerHealth = target.GetComponent<Health>();
        if (towerHealth != null)
        {
            towerHealth.TakeDamage(drainRate * Time.deltaTime);
        }
    }
}
