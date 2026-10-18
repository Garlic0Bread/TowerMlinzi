using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public Transform target;

    [SerializeField] private float speed = 15f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float lifetime = 3f;

    [SerializeField] private bool isPiercing = false;
    [SerializeField] private int maxPierces = 1;
    private int pierceCount = 0;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            if (!isPiercing || (isPiercing && pierceCount >= maxPierces))
            {
                Destroy(gameObject);
            }
            else
            {
                pierceCount++;
            }
        }
    }
}