using UnityEngine;

public class ShooterEnemy : EnemyAI
{
    [SerializeField] private GameObject projectilePrefab;
    //[SerializeField] private float fireRate = 2f;
    private float nextFire;

    protected override void OnReachTarget()
    {
        rb.linearVelocity = Vector3.zero; // Stop to shoot
        if (Time.time >= nextFire)
        {
            Shoot();
            nextFire = Time.time + attackRate;
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, transform.position + Vector3.up, transform.rotation);
    }
}
