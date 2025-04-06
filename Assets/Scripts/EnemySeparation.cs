using UnityEngine;

public class EnemySeparation : MonoBehaviour
{
    public float separationRadius = 1.5f;
    public float repulsionStrength = 2f;

    void Update()
    {
        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, separationRadius);
        Vector3 repulsion = Vector3.zero;

        foreach (var col in nearbyEnemies)
        {
            if (col.gameObject == gameObject) continue;
            if (!col.CompareTag("Enemy")) continue;

            Vector3 away = transform.position - col.transform.position;
            float distance = away.magnitude;

            if (distance > 0 && distance < separationRadius)
            {
                // Inverse force falloff
                repulsion += away.normalized / distance;
            }
        }

        if (repulsion != Vector3.zero)
        {
            // Slightly push this enemy away from others
            transform.position += repulsion * repulsionStrength * Time.deltaTime;
        }
    }
}
