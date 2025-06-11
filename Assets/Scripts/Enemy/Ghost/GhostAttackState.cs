using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttackState : EnemyState
{
    private Ghost ghost;

    public GhostAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Ghost ghostRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        ghost = ghostRef;
    }

    public override void Enter()
    {
        base.Enter();
        DelayTime = 0.1f;
    }

    public override void Exit()
    {
        base.Exit();
        ghost.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        if (ghost.isDead)
        {
            stateMachine.ChangeState(ghost.DeathState); 
            return;
        }
        base.Update();

        if (DelayTime > 0)
        {
            if (ghost.isKnockbacked)
            {
                DelayTime = 0;
                return;
            }
        }
        else
        {
            ghost.SetVelocity(0, rb.velocity.y);
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(ghost.BattleState);
        }
    }
}
