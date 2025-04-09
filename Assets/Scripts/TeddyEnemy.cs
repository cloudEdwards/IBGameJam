using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class TeddyEnemy : MonoBehaviour
{
    public int damage = 1;
    public float attackCooldown = 1f;
    public GameObject teddy;
    public GameObject teddyMonster;

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


        // flip scale depending on travel direction
        Vector3 newScale = transform.localScale;
        Vector3 posDiff = player.position - transform.position;

        if (posDiff.x > .1f) {
            newScale.x = 1;
        }

        if (posDiff.x < -.1f) {
            newScale.x = -1;
        }

        transform.localScale = newScale;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && attackTimer <= 0f)
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
                health.TakeDamage(damage);

            attackTimer = attackCooldown;

            StartCoroutine(AttackAnim());

        }
    }

    IEnumerator AttackAnim()
    {
        teddy.SetActive(false);
        teddyMonster.SetActive(true);

        yield return new WaitForSeconds(attackCooldown);

        teddy.SetActive(true);
        teddyMonster.SetActive(false);
    }
    
}
