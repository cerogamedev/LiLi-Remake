using UnityEngine;

public class BasicTurret : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 3f;
    private float nextFireTime = 0f;

    void Update()
    {
        if (nextFireTime >= fireRate )
        {
            FireBullet();
            nextFireTime = 0;
        }
        nextFireTime += 1;
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce(transform.up * 500f);
        Destroy(bullet, 8f);

    }
}
