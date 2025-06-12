using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMoveState : GhostGroundedState
{
    private float moveDuration;
    public GhostMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Ghost ghostRef)
        : base(enemyBase, stateMachine, animBoolName, ghostRef)
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
        var detection = ghost.IsPlayerDetected();

        if (detection.collider != null)
        {
            stateMachine.ChangeState(ghost.BattleState);
            return;
        }

        moveDuration -= Time.deltaTime;

        ghost.SetVelocity(ghost.patrolMoveSpeed * ghost.facingDirection, rb.velocity.y);

        if (moveDuration <= 0)
        {
            stateMachine.ChangeState(ghost.IdleState);
            return;
        }

        if (ghost.IsWallDetected() || !ghost.IsGroundDetected())
        {
            stateMachine.ChangeState(ghost.IdleState);
            return;
        }
    }
}
