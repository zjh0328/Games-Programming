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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        var detection = boss.IsPlayerDetected();

        if (detection.collider != null)
        {
            boss.EnterBattleState();
            return;
        }

        boss.SetVelocity(0, rb.velocity.y);
    }
}
