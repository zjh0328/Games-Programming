using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMoveState : ArcherGroundedState
{
    private float moveDuration;
    public ArcherMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Archer archerRef)
        : base(enemyBase, stateMachine, animBoolName, archerRef)
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
    }

    public override void Update()
    {
        base.Update();
        var detection = archer.IsPlayerDetected();

        if (detection.collider != null)
        {
            stateMachine.ChangeState(archer.BattleState);
            return;
        }

        moveDuration -= Time.deltaTime;

        archer.SetVelocity(archer.patrolMoveSpeed * archer.facingDirection, rb.velocity.y);

        if (moveDuration <= 0)
        {
            stateMachine.ChangeState(archer.IdleState);
            return;
        }

        if (archer.IsWallDetected() || !archer.IsGroundDetected())
        {
            stateMachine.ChangeState(archer.IdleState);
            return;
        }

    }
}
