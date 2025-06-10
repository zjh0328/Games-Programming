using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : BossGroundedState
{
    public BossMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Boss bossRef)
        : base(enemyBase, stateMachine, animBoolName, bossRef)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        var detection = boss.IsPlayerDetected();

        if (detection.collider != null)
        {
            boss.EnterBattleState();
            return;
        }


        if (boss.stateMachine.CurrentState != this)
            return;

        if (boss.IsWallDetected() || !boss.IsGroundDetected())
        {
            stateMachine.ChangeState(boss.IdleState);
            return;
        }

        boss.SetVelocity(boss.patrolMoveSpeed * boss.facingDirection, rb.velocity.y);
    }
}
