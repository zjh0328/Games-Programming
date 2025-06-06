using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttackState : EnemyState
{
    private Archer archer;

    public ArcherAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Archer archerRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        archer = archerRef;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(6, archer.transform);
        DelayTime = 0.2f;
    }

    public override void Exit()
    {
        base.Exit();
        archer.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (DelayTime > 0)
        {
            if (archer.isKnockbacked)
            {
                DelayTime = 0;
                return;
            }
        }
        else
        {
            archer.SetVelocity(0, rb.velocity.y);
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(archer.BattleState);
        }
    }
}
