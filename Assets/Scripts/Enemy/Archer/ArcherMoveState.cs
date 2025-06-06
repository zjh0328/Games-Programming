using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMoveState : ArcherGroundedState
{
    public ArcherMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Archer archerRef)
        : base(enemyBase, stateMachine, animBoolName, archerRef)
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

        if (archer.stateMachine.CurrentState != this)
            return;

        if (archer.IsWallDetected() || !archer.IsGroundDetected())
        {
            stateMachine.ChangeState(archer.IdleState);
            return;
        }

        archer.SetVelocity(archer.patrolMoveSpeed * archer.facingDirection, rb.velocity.y);
    }
}
