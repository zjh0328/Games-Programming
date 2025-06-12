using System.Collections;
using UnityEngine;

public class BossFullSkillState : EnemyState
{
    private Boss boss;

    public BossFullSkillState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Boss bossRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        boss = bossRef;
    }

    public override void Enter()
    {
        base.Enter();
        boss.anim.speed = 0;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
