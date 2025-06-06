using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected Player player;

    public float cooldown;
    protected float cooldownTimer;
    public float skillLastUseTime { get; protected set; } = 0;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool SkillReady()
    {
        if (cooldownTimer < 0)
        {
            return true;
        }
        else
        {
            player.fx.CreateText("Skill is in cooldown");
            return false;
        }
    }

    public virtual bool AvailableSkill()
    {
        if (cooldownTimer < 0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }
        player.fx.CreateText("Skill is in cooldown");
        return false;
    }

    public virtual void UseSkill()
    {

    }

    protected virtual Transform FindClosestEnemy(Transform searchCenter)
    {
        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        Collider2D[] hits = Physics2D.OverlapCircleAll(searchCenter.position, 12f);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                float distance = Vector2.Distance(searchCenter.position, hit.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestEnemy = hit.transform;
                }
            }
        }

        return closestEnemy;
    }

}
