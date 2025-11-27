using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Components")]
    protected EnemyMovement movement;

    [Header("Health")]
    [SerializeField] protected int maxHealth;
    protected int health;

    [Header("Elements")]
    protected Player player;


    [Header("Spawn Sequence Related")]
    [SerializeField] protected SpriteRenderer renderer;
    [SerializeField] protected SpriteRenderer spawnIndicator;
    [SerializeField] protected Collider2D collider;
    protected bool hasSpawned = false;

    [Header("Effects")]
    [SerializeField] protected ParticleSystem passAwayParticles;

    [Header("Attack")]
    [SerializeField] protected float playerDetectionRadius;

    [Header("Actions")]
    //action - position -  isCritical
    public static Action<int, Vector2, bool> onDamageTaken;

    //Position
    public static Action<Vector2> onPassedAway;
    public static Action<Vector2> onBossPassedAway;
    protected Action onSpawnSequenceCompleted;

    [Header("DEBUG")]
    [SerializeField] protected bool gizmos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        health = maxHealth;
        movement = GetComponent<EnemyMovement>();
        player = FindFirstObjectByType<Player>();

        if (player == null)
        {
            Debug.LogWarning("No player, auto-destroying");

            Destroy(gameObject);
        }

        StartSpawnSequence();
    }

    // Update is called once per frame
    protected bool CanAttack()
    {
        return renderer.enabled;
    }

    protected virtual void StartSpawnSequence()
    {
        SetRendererVisibility(false);

        //Scale up & down the spawn indicator
        Vector3 targetScale = spawnIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, 0.3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);
    }

    protected virtual void SpawnSequenceCompleted()
    {
        SetRendererVisibility(true);
        hasSpawned = true;

        collider.enabled = true;

        if (movement != null) 
            movement.StorePlayer(player);

        onSpawnSequenceCompleted?.Invoke();
    }

    protected virtual void SetRendererVisibility(bool visibility)
    {
        renderer.enabled = visibility;
        spawnIndicator.enabled = !visibility;

    }


    public void TakeDamage(int damage, bool isCriticalHit)  
    {
        int realDamage = Mathf.Min(health, damage);
        health -= realDamage;


        onDamageTaken?.Invoke(damage, transform.position, isCriticalHit);

        if (health <= 0)
        {
            PassAway();
        }
    }

    public virtual void PassAway()
    {
        onPassedAway?.Invoke(transform.position);

        PassAwayAfterWave();
    }

    public void PassAwayAfterWave()
    {
        passAwayParticles.transform.SetParent(null);

        passAwayParticles.Play();

        Destroy(gameObject);
    }

    public Vector2 GetCenter()
    {
        return (Vector2)transform.position + collider.offset;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        if (!gizmos) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
