using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleState : EnemyState
{
    private Transform player;
    private int moveDirection;
    private Boss boss;

    public BossBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Boss bossRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        boss = bossRef;
    }

    public override void Enter()
    {
        base.Enter();

        DelayTime = boss.aggressiveTime;
        player = PlayerManager.instance.player.transform;

        FacePlayer();

        if (player == null || player.GetComponent<PlayerStats>().currentHP <= 0)
        {
            return; 
        }

    }

    public override void Update()
    {
        base.Update();

        if (boss.stateMachine.CurrentState != this)
            return;

        FacePlayer();

        var detection = boss.IsPlayerDetected();

        if (boss.CanUseFullSkill())
        {
            boss.MarkFullSkillUsed();
            ChangeToIdleAnimation();
            stateMachine.ChangeState(boss.FullSkillState);
            return;
        }

        if (detection.collider != null)
        {
            DelayTime = boss.aggressiveTime;
            if (detection.distance < boss.attackDistance && CanAttack())
            {
                ChangeToIdleAnimation();
                if (Random.value < 0.3f)
                {
                    stateMachine.ChangeState(boss.DoubleAttackState);
                }
                else
                {
                    stateMachine.ChangeState(boss.AttackState);
                }

                return;
            }
        }
        else
        {
            float distanceToPlayer = Vector2.Distance(player.position, boss.transform.position);
            if (DelayTime < 0 || distanceToPlayer > boss.playerScanDistance)
            {
                stateMachine.ChangeState(boss.IdleState);
                return;
            }
        }

        if (detection.collider != null &&
            Vector2.Distance(boss.transform.position, player.position) < boss.attackDistance)
        {
            ChangeToIdleAnimation();
            return;
        }

        moveDirection = player.position.x >= boss.transform.position.x ? 1 : -1;

        if (!boss.IsGroundDetected())
        {
            boss.SetVelocity(0, rb.velocity.y);
            ChangeToIdleAnimation();
            return;
        }

        boss.SetVelocity(boss.battleMoveSpeed * moveDirection, rb.velocity.y);
        ChangeToMoveAnimation();
    }

    private bool CanAttack()
    {
        return Time.time - boss.lastTimeAttacked >= boss.attackCooldown && !boss.isKnockbacked && Mathf.Abs(rb.velocity.y) <= 0.1f;
    }

    private void ChangeToIdleAnimation()
    {
        if (!anim.GetBool("Idle"))
        {
            anim.SetBool("Move", false);
            anim.SetBool("Idle", true);
        }
    }

    private void ChangeToMoveAnimation()
    {
        if (!anim.GetBool("Move"))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Move", true);
        }
    }

    private void FacePlayer()
    {
        float deltaX = player.position.x - boss.transform.position.x;
        if ((deltaX < 0 && boss.facingDirection != -1) || (deltaX > 0 && boss.facingDirection != 1))
        {
            boss.Flip();
        }
    }
}
