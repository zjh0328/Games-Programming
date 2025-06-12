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
        DelayTime = 2f; 
        ghost.SetVelocity(0, rb.velocity.y);
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

        ghost.SetVelocity(0, rb.velocity.y);

        if (DelayTime <= 0)
        {
            int randomDir = Random.value < 0.5f ? -1 : 1;
            if (ghost.facingDirection != randomDir || !ghost.IsGroundDetected())
            {
                ghost.Flip(); 
            }
            stateMachine.ChangeState(ghost.MoveState);
        }
    }
}
