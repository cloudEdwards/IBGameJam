using _Project.Script.AbilitySystem;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public int health = 20;

    public AbilityTargeting TargetType { get; } = AbilityTargeting.Hostile;

    public void TakeDamage(float amount)
    {
        health -= (int)amount;
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
