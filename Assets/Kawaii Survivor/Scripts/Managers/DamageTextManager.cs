using System;
using UnityEngine;
using UnityEngine.Pool;
public class DamageTextManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private DamageText damageTextPrefab;

    [Header("Pooling")]
    private ObjectPool<DamageText> damageTextPool;

    private void Awake()
    {
        MeleeEnemy.onDamageTaken += EnemyHitCallback;
        PlayerHealth.onAttackDodged += AttackDodgeCallback;
    }

    private void OnDestroy()
    {
        MeleeEnemy.onDamageTaken -= EnemyHitCallback;
        PlayerHealth.onAttackDodged -= AttackDodgeCallback;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damageTextPool = new ObjectPool<DamageText>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    private DamageText CreateFunction()
    {
        return Instantiate(damageTextPrefab, transform);
    }

    private void ActionOnGet(DamageText damageText)
    {
        damageText.gameObject.SetActive(true);
    }

    private void ActionOnRelease(DamageText damageText)
    {
        damageText.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(DamageText damageText)
    {
        Destroy(damageText.gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyHitCallback(int damage, Vector2 enemyPos, bool isCriticalHit)
    {
        DamageText damageTextInstantiate = damageTextPool.Get();

        Vector3 spawnPosition = enemyPos + Vector2.up * 1.5f;
        damageTextInstantiate.transform.position = spawnPosition;

        damageTextInstantiate.Animate(damage.ToString(), isCriticalHit);

        LeanTween.delayedCall(1, () => damageTextPool.Release(damageTextInstantiate));
    }

    private void AttackDodgeCallback(Vector2 playerPosition)
    {
        DamageText damageTextInstantiate = damageTextPool.Get();

        Vector3 spawnPosition = playerPosition + Vector2.up * 1.5f;
        damageTextInstantiate.transform.position = spawnPosition;

        damageTextInstantiate.Animate("Dodge", false);

        LeanTween.delayedCall(1, () => damageTextPool.Release(damageTextInstantiate));
    }
}
