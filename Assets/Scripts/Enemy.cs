using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 20;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Enemy took damage! Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
