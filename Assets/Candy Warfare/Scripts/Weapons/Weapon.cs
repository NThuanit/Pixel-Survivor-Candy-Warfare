using NUnit.Framework;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IPlayerStatsDependency
{
    [field: SerializeField] public WeaponDataSO WeaponData { get; private set; }

    [Header("Settings")]
    [SerializeField] protected float range;
    [SerializeField] protected LayerMask enemyMask;

    [Header("Attack")]
    [SerializeField] protected int damage;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected Animator animator;
    protected float attackTimer;

    [Header("Critical")]
    protected int criticalChance;
    protected float criticalPersent;

    [Header("Animations")]
    [SerializeField] protected float aimLerp;

    [Header("Level")]
    [field: SerializeField] public int Level { get; private set; }

    // --- XÓA HEADER AUDIO VÀ AUDIOSOURCE Ở ĐÂY (Không cần nữa) ---

    private void Awake()
    {
        // --- XÓA ĐOẠN TẠO AUDIOSOURCE Ở ĐÂY ---

        // Chỉ giữ lại phần Animation
        if (animator != null && WeaponData.AnimatorOverride != null)
            animator.runtimeAnimatorController = WeaponData.AnimatorOverride;
    }

    protected void PlayeAttackSound()
    {
        // --- SỬA LỖI Ở ĐÂY ---
        // Thay vì tự kiểm tra và tự phát, hãy gọi AudioManager
        if (AudioManager.instance != null)
        {
            // AudioManager sẽ tự biết có đang Mute hay không để phát
            AudioManager.instance.PlaySFX(WeaponData.AttackSound);
        }
    }

    protected Enemy GetClosestEnemy()
    {
        Enemy closestEnemy = null;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

        if (enemies.Length <= 0)
        {
            return null;
        }

        float minDistance = range;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemyChecked = enemies[i].GetComponent<Enemy>();

            float distanceToEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);

            if (distanceToEnemy < minDistance)
            {
                closestEnemy = enemyChecked;
                minDistance = distanceToEnemy;
            }
        }

        return closestEnemy;
    }

    //cirtial hit
    protected int GetDamage(out bool isCiriticalHit)
    {
        isCiriticalHit = false;

        if (Random.Range(0, 101) <= criticalChance)
        {
            isCiriticalHit = true;
            return Mathf.RoundToInt(damage * criticalPersent);
        }

        return damage;
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);

    }

    public abstract void UpdateStats(PlayerStatsManager statsManager);

    protected void ConfiguresStats()
    {
        Dictionary<Stat, float> calculatedStats = WeaponStatsCalculator.GetStats(WeaponData, Level);

        damage = Mathf.RoundToInt(calculatedStats[Stat.Attack]);
        attackDelay = 1f / (calculatedStats[Stat.AttackSpeed]);
        criticalChance = Mathf.RoundToInt(calculatedStats[Stat.CriticalChance]);
        criticalPersent = calculatedStats[Stat.CriticalPercent];
        range = calculatedStats[Stat.Range];
    }

    public void UpgradeTo(int targetLevel)
    {
        Level = targetLevel;
        ConfiguresStats();
    }

    public int GetRecyclePrice()
    {
        return WeaponStatsCalculator.GetRecyclePrice(WeaponData, Level);
    }

    public void Upgrade() => UpgradeTo(Level + 1);
}