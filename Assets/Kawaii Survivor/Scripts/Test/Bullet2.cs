using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    private Pooling pool;
    private bool isReleased = false;

    private void OnEnable()
    {
        isReleased = false;
    }

    public void Initialize(Pooling pool)
    {
        this.pool = pool;
        isReleased = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bullet hit: " + other.name); // 👉 In ra khi va chạm

        if (isReleased) return;

        isReleased = true;
        pool.ReleaseBullet(this);
    }

}
