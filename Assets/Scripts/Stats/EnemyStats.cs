using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;

    [Header("Enemy Level (1: Easy, 2: Medium, 3: Hard)")]
    [SerializeField] private int enemyLevel = 1;

    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
        enemyLevel = LevelManager.instance.GetEnemyLevel();
        ApplyLevelScaling();
    }

    private void ApplyLevelScaling()
    {
        switch (enemyLevel)
        {
            case 1:
                maxHP = 50;
                damage = 10;
                break;
            case 2:
                maxHP = 100;
                damage = 20;
                break;
            case 3:
                maxHP = 150;
                damage = 30;
                break;
        }

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
