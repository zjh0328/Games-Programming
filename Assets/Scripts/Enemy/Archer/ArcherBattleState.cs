using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBattleState : EnemyState
{
    private Transform player;
    private Archer archer;

    public ArcherBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Archer archerRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        archer = archerRef;
    }

    public override void Enter()
    {
        base.Enter();

        DelayTime = archer.aggressiveTime;
        player = PlayerManager.instance.player.transform;

        FacePlayer();

        if (player.GetComponent<PlayerStats>().isDead)
        {
            stateMachine.ChangeState(archer.MoveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        if(archer.stats.currentHP <= 0)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Move", false);
            stateMachine.ChangeState(archer.DeathState);
            return;
        }
        base.Update();

        if (archer.stateMachine.CurrentState != this)
            return;

        FacePlayer();

        bool playerDetected = archer.IsPlayerDetected();
        float distance = Vector2.Distance(archer.transform.position, player.position);

        if (playerDetected)
        {
            DelayTime = archer.aggressiveTime;

            if (distance < archer.attackDistance && CanAttack())
            {
                anim.SetBool("Idle", false);
                anim.SetBool("Move", false);
                stateMachine.ChangeState(archer.AttackState);
                return;
            }

            if (distance < archer.attackDistance)
            {
                ChangeToIdleAnimation();
            }
        }
        else if (DelayTime < 0 || distance > archer.playerScanDistance)
        {
            stateMachine.ChangeState(archer.IdleState);
        }
    }


    private bool CanAttack()
    {
        if (Time.time - archer.lastTimeAttacked >= archer.attackCooldown && !archer.isKnockbacked)
        {
            return true;
        }

        return false;
    }

    private void ChangeToIdleAnimation()
    {
        anim.SetBool("Move", false);
        anim.SetBool("Idle", true);
    }

    private void FacePlayer()
    {
        float dir = player.position.x - archer.transform.position.x;
        int desiredFacing = dir > 0 ? 1 : -1;

        if (archer.facingDirection != desiredFacing)
        {
            archer.Flip();
        }
    }

}
