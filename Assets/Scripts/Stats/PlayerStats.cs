using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void DoDamage(CharacterStats targetStats)
    {
        base.DoDamage(targetStats);
    }

    protected override void Die()
    {
        base.Die();
        player.Die();
    }

    public override int DecreaseHPBy(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);

        onHealthChanged?.Invoke();

        return damage;
    }
}
