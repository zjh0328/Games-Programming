using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherIdleState : ArcherGroundedState
{
    public ArcherIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Archer archerRef)
        : base(enemyBase, stateMachine, animBoolName, archerRef)
    {
    }

    public override void Enter()
    {
        base.Enter();
        DelayTime = archer.patrolTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.CurrentState != archer.IdleState)
        {
            return;
        }

        archer.SetVelocity(0, rb.velocity.y);

        if (DelayTime < 0)
        {
            archer.Flip();
            stateMachine.ChangeState(archer.MoveState);
        }
    }
}
