using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SwarmerEnemy : MonoBehaviour
{
    public int damage = 1;
    public float attackCooldown = 1f;

    private Transform player;
    private NavMeshAgent agent;
    private float attackTimer;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        
        agent.updateRotation = false;  // <-- prevent rotation
    }

    void Update()
    {
        if (player == null) return;

        attackTimer -= Time.deltaTime;

        // Constantly move toward the player
        agent.SetDestination(player.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && attackTimer <= 0f)
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
                health.TakeDamage(damage);

            attackTimer = attackCooldown;
        }
    }
}
