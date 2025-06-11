using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundedState
{
    private float moveDuration;

    public SkeletonMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Skeleton skeletonRef)
        : base(enemyBase, stateMachine, animBoolName, skeletonRef)
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
        var detection = skeleton.IsPlayerDetected();
        if (detection.collider != null)
        {
            stateMachine.ChangeState(skeleton.BattleState);
            return;
        }

        if (!skeleton.IsGroundDetected())
        {
            skeleton.SetVelocity(0, rb.velocity.y); 
            stateMachine.ChangeState(skeleton.IdleState);
            return;
        }

        moveDuration -= Time.deltaTime;

        skeleton.SetVelocity(skeleton.patrolMoveSpeed * skeleton.facingDirection, rb.velocity.y);

        if (moveDuration <= 0)
        {
            stateMachine.ChangeState(skeleton.IdleState);
            return;
        }

        if (skeleton.IsWallDetected() || !skeleton.IsGroundDetected())
        {
            skeleton.Flip();
            stateMachine.ChangeState(skeleton.IdleState);
            return;
        }
    }
}
