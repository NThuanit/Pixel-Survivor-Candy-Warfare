using UnityEngine;

[RequireComponent (typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyBullet : MonoBehaviour
{
    [Header("Elements")]
    private Rigidbody2D rig;
    private Collider2D collider;
    private RangeEnemyAttack rangeEnemyAttack;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float angularSpeed;
    private int damage;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        LeanTween.delayedCall(gameObject, 5, () => rangeEnemyAttack.ReleaseBullet(this));
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public void Configure(RangeEnemyAttack rangeEnemyAttack)
    {
        this.rangeEnemyAttack = rangeEnemyAttack;
    }

    public void Shoot(int damage, Vector2 direction)
    {
        this.damage = damage;

        if (Mathf.Abs(direction.x + 1) < 0.01f)
            direction.y += 0.01f;

        transform.right = direction;    

        rig.linearVelocity = direction * moveSpeed;
        rig.angularVelocity = angularSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            LeanTween.cancel(gameObject);
            player.TakeDamage(this.damage);
            this.collider.enabled = false;
            rangeEnemyAttack.ReleaseBullet(this);
        }
    }

    public void ReLoad()
    {
        rig.linearVelocity = Vector2.one;
        rig.angularVelocity = 0;
        this.collider.enabled = true;

        LeanTween.cancel(gameObject);
        LeanTween.delayedCall(gameObject, 5,  () => rangeEnemyAttack.ReleaseBullet(this));
    }
}
