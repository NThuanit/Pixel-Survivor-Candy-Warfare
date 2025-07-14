using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindFirstObjectByType<ObjectPool>().ReturnObject(gameObject);
    }
}
