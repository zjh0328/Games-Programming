using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundedState
{
    public SkeletonIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Skeleton skeletonRef)
        : base(enemyBase, stateMachine, animBoolName, skeletonRef)
    {
    }

    public override void Enter()
    {
        base.Enter();
        DelayTime = skeleton.patrolTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.CurrentState != skeleton.IdleState)
        {
            return;
        }

        skeleton.SetVelocity(0, rb.velocity.y);

        if (DelayTime < 0)
        {
            skeleton.Flip();
            stateMachine.ChangeState(skeleton.MoveState);
        }
    }
}
