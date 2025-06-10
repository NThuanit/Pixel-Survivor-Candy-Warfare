using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [Header("Components")]
    private EnemyMovement movement;

    [Header("Elements")]
    private Player player;

    [Header("Spawn Sequence Related")]
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    private bool hasSpawned = false;

    [Header("Effects")]
    [SerializeField] private ParticleSystem passAwayParticles;

    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    [SerializeField] private float playerDetectionRadius;
    private float attackDelay;
    private float attackTimer;

    [Header("DEBUG")]
    [SerializeField] private bool gizmos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = GetComponent<EnemyMovement>();   
        player = FindFirstObjectByType<Player>();

        if (player == null)
        {
            Debug.LogWarning("No player, auto-destroying");

            Destroy(gameObject);
        }

        StartSpawnSequence();
        attackDelay = 1f / attackFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer >= attackDelay)
        {
            TryAttack();
        }
        else
        {
            Wait();
        }
    }

    private void StartSpawnSequence()
    {
        SetRendererVisibility(false);

        //Scale up & down the spawn indicator
        Vector3 targetScale = spawnIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, 0.3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);
    }

    private void SpawnSequenceCompleted()
    {
        SetRendererVisibility(true);
        hasSpawned = true;

        movement.StorePlayer(player);
    }

    private void SetRendererVisibility(bool visibility)
    {
        renderer.enabled = visibility;
        spawnIndicator.enabled = !visibility;

    }

    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);


        if (distanceToPlayer <= playerDetectionRadius)
        {
            Attack();
        }
    }

    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }

    private void Attack()
    {
        attackTimer = 0;
        player.TakeDamage(damage);
    }
    private void PassAway()
    {
        //unparent the particles and play them

        passAwayParticles.transform.SetParent(null);

        passAwayParticles.Play();

        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        if (!gizmos) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
