using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherDeathState : EnemyState
{
    private Archer archer;

    public ArcherDeathState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Archer archerRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        archer = archerRef;
    }

    public override void Enter()
    {
        base.Enter();
        archer.anim.speed = 0;
        archer.cd.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
