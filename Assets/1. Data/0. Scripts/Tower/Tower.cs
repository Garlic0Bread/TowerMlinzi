using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 1000f;

    void Awake() => currentHealth = maxHealth;

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            // Trigger Fail State
            Debug.Log("The Historical Site has been destroyed!");
        }
    }
}
