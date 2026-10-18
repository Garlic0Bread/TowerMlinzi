using UnityEngine;

public class MarchingWarrior : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 50f;
    [SerializeField] private float lifetime = 4f;
    [SerializeField] private float knockbackForce = 20f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);

                // Push them back
                Rigidbody enemyRb = other.GetComponent<Rigidbody>();
                if (enemyRb != null)
                {
                    Vector3 pushDir = transform.forward + Vector3.up * 0.5f;
                    enemyRb.AddForce(pushDir * knockbackForce, ForceMode.Impulse);
                }
            }
        }
    }
}
