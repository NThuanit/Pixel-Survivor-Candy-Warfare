using UnityEngine;

public class RangeEnemyAttack : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameManager bulletPrefab;
    private Player player;

    [Header("Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    private float attackDelay;
    private float attackTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackDelay = 1f / attackFrequency;
        attackTimer = attackDelay;
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StorePlayer(Player player)
    {
        this.player = player;
    }

    public void AutoAim()
    {

        ManageShooting();
    }

    private void ManageShooting()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackDelay)
        {
            attackTimer = 0;
            Shoot();
        }
    }

    Vector2 gizmosDirection;
    private void Shoot()
    {
        Vector2 direction = (player.GetCenter() - (Vector2)shootingPoint.position).normalized;
        gizmosDirection = direction;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(shootingPoint.position, (Vector2)shootingPoint.position + gizmosDirection * 5);
    }
}
