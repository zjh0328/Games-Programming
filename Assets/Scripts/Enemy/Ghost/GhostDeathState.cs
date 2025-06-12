using UnityEngine;

public class GhostDeathState : EnemyState
{
    private Ghost ghost;

    public GhostDeathState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Ghost ghostRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        ghost = ghostRef;
    }

    public override void Enter()
    {
        base.Enter();
        ghost.SetVelocity(0, 0);
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
