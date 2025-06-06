using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeathState : EnemyState
{
    private Skeleton skeleton;

    public SkeletonDeathState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Skeleton skeletonRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        skeleton = skeletonRef;
    }

    public override void Enter()
    {
        base.Enter();
        skeleton.anim.speed = 0;
        skeleton.cd.enabled = false;
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
