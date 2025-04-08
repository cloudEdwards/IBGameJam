using UnityEngine;

public class Weapon : MonoBehaviour 
{
    public GameObject bulletPrefab;

    public void FireWeapon(Vector3 direction, Transform firePoint, Quaternion rotation)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
        bullet.GetComponent<Bullet>().Initialize(direction);
    }

}