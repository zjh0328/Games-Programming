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
        DelayTime = 2f; 
        boss.SetVelocity(0, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        boss.TryEnterFullSkillState();
    }

    public override void Update()
    {
        base.Update();
        var detection = boss.IsPlayerDetected();

        if (detection.collider != null)
        {
            stateMachine.ChangeState(boss.BattleState);
            return;
        }

        boss.SetVelocity(0, rb.velocity.y);

        if (DelayTime <= 0)
        {
            int randomDir = Random.value < 0.5f ? -1 : 1;
            if (boss.facingDirection != randomDir || !boss.IsGroundDetected())
            {
                boss.Flip(); 
            }
            stateMachine.ChangeState(boss.MoveState);
        }
    }
}
