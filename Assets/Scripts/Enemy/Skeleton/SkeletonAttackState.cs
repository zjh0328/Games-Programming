using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    private Skeleton skeleton;

    public SkeletonAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Skeleton skeletonRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        skeleton = skeletonRef; 
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        skeleton.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            stateMachine.ChangeState(skeleton.BattleState);
        }
    }
}
