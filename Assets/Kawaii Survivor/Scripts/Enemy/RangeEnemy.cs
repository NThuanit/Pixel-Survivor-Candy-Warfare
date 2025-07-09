using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(EnemyMovement), typeof(RangeEnemyAttack))]    
public class RangeEnemy : MonoBehaviour
{
    [Header("Components")]
    private EnemyMovement movement;
    private RangeEnemyAttack attack;    

    [Header("Health")]
    [SerializeField] private int maxHealth;
    private int health;

    [Header("Elements")]
    private Player player;

    [Header("Spawn Sequence Related")]
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    [SerializeField] private Collider2D collider;
    private bool hasSpawned = false;

    [Header("Effects")]
    [SerializeField] private ParticleSystem passAwayParticles;

    [Header("Attack")]
    [SerializeField] private float playerDetectionRadius;

    [Header("Actions")]
    public static Action<int, Vector2> onDamageTaken;

    [Header("DEBUG")]
    [SerializeField] private bool gizmos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;

        movement = GetComponent<EnemyMovement>();
        attack = GetComponent<RangeEnemyAttack>();

        player = FindFirstObjectByType<Player>();

        attack.StorePlayer(player);

        if (player == null)
        {
            Debug.LogWarning("No player, auto-destroying");

            Destroy(gameObject);
        }

        StartSpawnSequence();
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

        collider.enabled = true;

        movement.StorePlayer(player);
    }

    private void SetRendererVisibility(bool visibility)
    {
        renderer.enabled = visibility;
        spawnIndicator.enabled = !visibility;

    }

    // Update is called once per frame
    void Update()
    {
        if (!renderer.enabled) return;

        ManageAttack();

    }

    private void ManageAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > playerDetectionRadius)
            movement.FollowPlayer();
        else TryAttack();
    }

    private void TryAttack()
    {
        attack.AutoAim();
    }

  
    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(health, damage);
        health -= realDamage;


        onDamageTaken?.Invoke(damage, transform.position);

        if (health <= 0)
        {
            PassAway();
        }
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
