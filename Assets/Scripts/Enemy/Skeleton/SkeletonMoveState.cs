using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundedState
{
    public SkeletonMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Skeleton skeletonRef)
        : base(enemyBase, stateMachine, animBoolName, skeletonRef)
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

        if (skeleton.stateMachine.CurrentState != this)
            return;

        if (skeleton.IsWallDetected() || !skeleton.IsGroundDetected())
        {
            stateMachine.ChangeState(skeleton.IdleState);
            return;
        }

        skeleton.SetVelocity(skeleton.patrolMoveSpeed * skeleton.facingDirection, rb.velocity.y);
    }
}
