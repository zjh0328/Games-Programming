using System;
using TMPro;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHP = 100;
    public int damage = 20;
    public int currentHP;
    public bool isInvincible { get; private set; }
    public bool isDead { get; private set; }

    public Action onHealthChanged;
    protected EntityFX fx;

    protected virtual void Awake()
    {
        fx = GetComponent<EntityFX>();
    }

    protected virtual void Start()
    {
        currentHP = maxHP;
    }

    public virtual void DoDamage(CharacterStats targetStats)
    {
        if (targetStats.isInvincible)
            return;

        targetStats.TakeDamage(damage, transform, targetStats.transform);
    }

    public virtual void TakeDamage(int amount, Transform attacker, Transform attackee)
    {
        if (isInvincible)
        {
            Debug.Log($"{gameObject.name} is invincible, damage ignored.");
            return;
        }

        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        fx.CreateHitFX(attackee);
        fx.CreateText(amount.ToString());
        attackee.GetComponent<Entity>()?.DamageKnockbackEffect(attacker, attackee);

        onHealthChanged?.Invoke();

        if (currentHP <= 0 && !isDead)
        {
            Die();
        }
    }
    public virtual int DecreaseHPBy(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        onHealthChanged?.Invoke();

        return amount;
    }
    public int getMaxHP()
    {
        return maxHP;
    }


    protected virtual void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} has died.");
    }

    public void Falling()
    {
        if (isDead) return;
        currentHP = 0;
        onHealthChanged?.Invoke();
        Die();
    }

    public void BecomeInvincible(bool invincible)
    {
        isInvincible = invincible;
    }
}
