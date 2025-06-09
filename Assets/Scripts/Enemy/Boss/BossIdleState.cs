using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossGroundedState
{
    public BossIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Boss bossRef)
        : base(enemyBase, stateMachine, animBoolName, bossRef)
    {
    }

    public override void Enter()
    {
        base.Enter();
        DelayTime = Boss.patrolTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.CurrentState != Boss.IdleState)
        {
            return;
        }

        Boss.SetVelocity(0, rb.velocity.y);

        if (DelayTime < 0)
        {
            Boss.Flip();
            stateMachine.ChangeState(Boss.MoveState);
        }
    }
}
