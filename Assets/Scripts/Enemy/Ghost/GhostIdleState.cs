using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostIdleState : GhostGroundedState
{
    public GhostIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Ghost ghostRef)
        : base(enemyBase, stateMachine, animBoolName, ghostRef)
    {
    }

    public override void Enter()
    {
        base.Enter();
        DelayTime = ghost.patrolTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.CurrentState != ghost.IdleState)
        {
            return;
        }

        ghost.SetVelocity(0, rb.velocity.y);

        if (DelayTime < 0)
        {
            ghost.Flip();
            stateMachine.ChangeState(ghost.MoveState);
        }
    }
}
