using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BruteEnemy : MonoBehaviour
{
    public int damage = 2;
    public float attackCooldown = 1f;
    public float chargeMinDistance = 5f;
    public float chargeSpeed = 2f;

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
        DoNpcBehaviour();
    }

    void DoNpcBehaviour()
    {
        if (player == null) return;

        attackTimer -= Time.deltaTime;

        // Constantly move toward the player
        agent.SetDestination(player.position);

        if (Vector3.Distance(player.position, gameObject.transform.position) <= chargeMinDistance) {
            agent.speed = chargeSpeed;
        }
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
