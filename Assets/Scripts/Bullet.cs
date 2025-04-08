using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public float lifeTime = 2f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Initialize(Vector3 direction)
    {
        Debug.Log("direction.normalized * speed");
        Debug.Log(direction.normalized * speed);
        Debug.Log("rb.velocity");
        Debug.Log(rb.linearVelocity);
        rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
        Debug.Log("rb.velocity after");
        Debug.Log(rb.linearVelocity);
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(damage);
        }

        if (! other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
        
    }
}
