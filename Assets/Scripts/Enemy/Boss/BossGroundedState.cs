using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGroundedState : EnemyState
{
    protected Transform player;
    protected Boss boss;

    public BossGroundedState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Boss bossRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        boss = bossRef;
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
            stateMachine.ChangeState(boss.BattleState);
        }
    }

    private bool ShouldEnterBattleState()
    {
        if (player == null) return false;

        bool playerDetected = boss.IsPlayerDetected();
        bool playerCloseEnough = Vector2.Distance(player.position, boss.transform.position) < boss.playerScanDistance;
        bool playerAlive = player.GetComponent<PlayerStats>()?.isDead == false;

        return (playerDetected || playerCloseEnough) && playerAlive;
    }
}
