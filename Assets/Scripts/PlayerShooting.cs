using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;   // Where the bullet spawns
    public float fireRate = 0.2f;

    private float fireCooldown;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (Input.GetButton("Fire1") && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;
        }
    }

    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);  // Y-up, XZ plane

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 direction = (hitPoint - firePoint.position);
            direction.y = 0f; // Ensure it's flat on the XZ plane
            direction.Normalize();

            // Snap to nearest 8-direction
            Vector3 snappedDirection = Get8Direction(direction);

            // Rotate bullet to face the direction
            Quaternion rotation = Quaternion.LookRotation(snappedDirection, Vector3.up);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
            bullet.GetComponent<Bullet>().Initialize(direction);
        }
    }

    Vector3 Get8Direction(Vector3 dir)
    {
        Vector3[] directions = new Vector3[]
        {
            new Vector3(1, 0, 0),    // East
            new Vector3(1, 0, 1).normalized,  // NE
            new Vector3(0, 0, 1),    // North
            new Vector3(-1, 0, 1).normalized, // NW
            new Vector3(-1, 0, 0),   // West
            new Vector3(-1, 0, -1).normalized, // SW
            new Vector3(0, 0, -1),   // South
            new Vector3(1, 0, -1).normalized  // SE
        };

        float maxDot = -Mathf.Infinity;
        Vector3 bestDir = directions[0];

        foreach (Vector3 d in directions)
        {
            float dot = Vector3.Dot(dir, d);
            if (dot > maxDot)
            {
                maxDot = dot;
                bestDir = d;
            }
        }

        return bestDir;
    }
}
