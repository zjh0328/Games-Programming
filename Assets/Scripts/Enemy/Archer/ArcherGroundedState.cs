using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherGroundedState : EnemyState
{
    protected Transform player;
    protected Archer archer;

    public ArcherGroundedState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Archer archerRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        archer = archerRef;
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
            stateMachine.ChangeState(archer.BattleState);
        }
    }

    private bool ShouldEnterBattleState()
    {
        if (player == null) return false;

        bool playerDetected = archer.IsPlayerDetected();
        bool playerCloseEnough = Vector2.Distance(player.position, archer.transform.position) < archer.playerScanDistance;
        bool playerAlive = player.GetComponent<PlayerStats>()?.isDead == false;

        return (playerDetected || playerCloseEnough) && playerAlive;
    }
}
