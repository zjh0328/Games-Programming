using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMoveState : GhostGroundedState
{
    public GhostMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Ghost ghostRef)
        : base(enemyBase, stateMachine, animBoolName, ghostRef)
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

        if (ghost.stateMachine.CurrentState != this)
            return;

        if (ghost.IsWallDetected() || !ghost.IsGroundDetected())
        {
            stateMachine.ChangeState(ghost.IdleState);
            return;
        }

        ghost.SetVelocity(ghost.patrolMoveSpeed * ghost.facingDirection, rb.velocity.y);
    }
}
