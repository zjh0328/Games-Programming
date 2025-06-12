using UnityEngine;

public class BossDeathState : EnemyState
{
    private Boss boss;

    public BossDeathState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Boss bossRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        boss = bossRef;
    }

    public override void Enter()
    {
        base.Enter();
        boss.anim.speed = 0;
        UI.instance.SwitchToThankYouText();
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
