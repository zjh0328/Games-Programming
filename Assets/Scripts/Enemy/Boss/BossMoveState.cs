using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : BossGroundedState
{
    private float moveDuration;
    public BossMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Boss bossRef)
        : base(enemyBase, stateMachine, animBoolName, bossRef)
    {
    }

    public override void Enter()
    {
        base.Enter();
        moveDuration = Random.Range(2f, 8f);
    }

    public override void Exit()
    {
        base.Exit();
        boss.TryEnterFullSkillState();
    }

    public override void Update()
    {
        base.Update();
        var detection = boss.IsPlayerDetected();

        if (detection.collider != null)
        {
            stateMachine.ChangeState(boss.BattleState);
            return;
        }

        moveDuration -= Time.deltaTime;

        boss.SetVelocity(boss.patrolMoveSpeed * boss.facingDirection, rb.velocity.y);

        if (moveDuration <= 0)
        {
            stateMachine.ChangeState(boss.IdleState);
            return;
        }

        if (boss.IsWallDetected() || !boss.IsGroundDetected())
        {
            stateMachine.ChangeState(boss.IdleState);
            return;
        }
    }
}
