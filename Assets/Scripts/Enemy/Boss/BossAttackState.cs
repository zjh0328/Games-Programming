using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : EnemyState
{
    private Boss boss;

    public BossAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Boss bossRef)  
        : base(enemyBase, stateMachine, animBoolName)
    {
        boss = bossRef;
    }

    public override void Enter()
    {
        base.Enter();
        DelayTime = 0.1f;
    }


    public override void Exit()
    {
        base.Exit();
        boss.lastTimeAttacked = Time.time;
        boss.TryEnterFullSkillState();
    }

    public override void Update()
    {
        base.Update();

        if (DelayTime <= 0)
        {
            boss.SetVelocity(0, rb.velocity.y);
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(boss.BattleState);
        }
    }
}
