using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;

    [Header("Enemy Level (1: Easy, 2: Medium, 3: Hard...)")]
    [SerializeField] private int enemyLevel = 1;

    [Header("base stats")]
    [SerializeField] private int baseHP = 50;
    [SerializeField] private int baseDamage = 10;

    [Header("growth ratios")]
    [SerializeField] private float hpGrowthRatio = 1f;        
    [SerializeField] private float damageGrowthRatio = 1f;    

    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();

        enemyLevel = LevelManager.instance.GetEnemyLevel();

        ApplyLevelScaling();
    }

    private void ApplyLevelScaling()
    {
        int level = Mathf.Max(enemyLevel, 1); 

        float multiplier = 1f + (level - 1) * hpGrowthRatio;
        maxHP = Mathf.RoundToInt(baseHP * multiplier);

        multiplier = 1f + (level - 1) * damageGrowthRatio;
        damage = Mathf.RoundToInt(baseDamage * multiplier);

        currentHP = maxHP;
    }

    public override void TakeDamage(int damage, Transform attacker, Transform attackee)
    {
        base.TakeDamage(damage, attacker, attackee);
        enemy.EnterBattleState();
    }

    public int GetEnemyLevel()
    {
        return enemyLevel;
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
        Destroy(gameObject, 3f);
    }

    public void ZeroHP()
    {
        currentHP = 0;
        base.Die();
        onHealthChanged?.Invoke();
    }
}
