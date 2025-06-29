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
        DelayTime = 2f;
        skeleton.SetVelocity(0, rb.velocity.y);
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

        if (DelayTime <= 0)
        {
            // Generated by ChatGPT, with some modifications for clarity and functionality.
            int randomDir = Random.value < 0.5f ? -1 : 1;
            if (skeleton.facingDirection != randomDir || !skeleton.IsGroundDetected())
                skeleton.Flip(); 
            stateMachine.ChangeState(skeleton.MoveState);
        }
    }
}
