using UnityEngine;
using UnityEngine.Pool;

public class Shootingg : MonoBehaviour
{
    [SerializeField] private Pooling pooling;
    [SerializeField] private GameObject enemy;

    [Header("Shooting Settings")]
    [SerializeField] private float fireRate = 0.5f; // thời gian giữa mỗi lần bắn (tính bằng giây)
    private float fireTimer;

    private void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f; // reset bộ đếm
        }
    }

    private void Shoot()
    {
        Bullet2 bullet = pooling.GetBullet();
        bullet.transform.position = transform.position;

        bullet.transform.up = (enemy.transform.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = bullet.transform.up * 5f;
    }
}
