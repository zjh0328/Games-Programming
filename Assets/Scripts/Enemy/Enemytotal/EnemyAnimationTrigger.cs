using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();

    private void AnimationTrigger()
    {
        enemy.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        enemy.AttackTrigger();
    }

    private void SpecialAttackTrigger()
    {
        enemy.SpecialAttackTrigger();
    }
    
    private void TriggerFullSkillDamage()
    {
        enemy.TriggerFullSkillDamage();
    }
}
