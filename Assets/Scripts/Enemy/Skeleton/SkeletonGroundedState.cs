using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected Transform player;
    protected Skeleton skeleton;

    public SkeletonGroundedState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Skeleton skeletonRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        skeleton = skeletonRef;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player?.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (ShouldEnterBattleState())
        {
            stateMachine.ChangeState(skeleton.BattleState);
            return;
        }
    }

    private bool ShouldEnterBattleState()
    {
        bool playerDetected = skeleton.IsPlayerDetected();
        bool playerCloseEnough = Vector2.Distance(player.position, skeleton.transform.position) < skeleton.CloseDistance;
        bool playerAlive = player.GetComponent<PlayerStats>()?.isDead == false;

        return (playerDetected || playerCloseEnough) && playerAlive;
    }
}
