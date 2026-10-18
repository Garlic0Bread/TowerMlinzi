using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;

    [Header("Events")]
    [SerializeField] private UnityEvent OnDeath;
    [SerializeField] private UnityEvent OnTakeDamage;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        OnTakeDamage?.Invoke();

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(float healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }

    void Die()
    {
        OnDeath?.Invoke();
        print("dead");
        //Destroy(gameObject);
    }
}
