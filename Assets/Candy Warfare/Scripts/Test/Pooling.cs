using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pooling : MonoBehaviour
{
    [Header("Pooling")]
    [SerializeField] private ObjectPool<Bullet2> bulletPool;
    [SerializeField] private Bullet2 bulletPrefab;

    private void Start()
    {
        bulletPool = new ObjectPool<Bullet2>(
            BulletCreateFunction,
            BulletActionOnGet,
            BulletActionOnRealse,
            BulletActionOnDestroy
           
        );
    }

    private Bullet2 BulletCreateFunction()
    {
        Bullet2 bullet = Instantiate(bulletPrefab);
        bullet.Initialize(this);
        return bullet;
    }

    private void BulletActionOnGet(Bullet2 bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void BulletActionOnRealse(Bullet2 bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void BulletActionOnDestroy(Bullet2 bullet)
    {
        Destroy(bullet.gameObject);
    }

    public Bullet2 GetBullet()
    {
        return bulletPool.Get();
    }

    public void ReleaseBullet(Bullet2 bullet)
    {
        bulletPool.Release(bullet);
    }
}
