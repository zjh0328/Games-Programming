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

        var detection = skeleton.IsPlayerDetected();

        if (detection.collider != null)
        {
            stateMachine.ChangeState(skeleton.BattleState);
            return;
        }

        skeleton.SetVelocity(0, rb.velocity.y);

        if (DelayTime <= 0)
        {
            int randomDir = Random.value < 0.5f ? -1 : 1;
            if (skeleton.facingDirection != randomDir || !skeleton.IsGroundDetected())
                skeleton.Flip(); 
            stateMachine.ChangeState(skeleton.MoveState);
        }
    }
}
