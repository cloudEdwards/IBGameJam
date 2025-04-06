using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public System.Action OnDeath;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Remaining: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} died!");

        OnDeath?.Invoke();

        // Add more death logic here (animations, effects, etc.)
        Destroy(gameObject);
    }
}
