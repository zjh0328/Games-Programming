using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGroundedState : EnemyState
{
    protected Transform player;
    protected Ghost ghost;

    public GhostGroundedState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Ghost ghostRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        ghost = ghostRef;
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
        if (ghost.isDead)
        {
            stateMachine.ChangeState(ghost.DeathState); 
            return;
        }
        base.Update();

        if (ShouldEnterBattleState())
        {
            stateMachine.ChangeState(ghost.BattleState);
        }
    }

    private bool ShouldEnterBattleState()
    {
        if (player == null) return false;

        bool playerDetected = ghost.IsPlayerDetected();
        bool playerCloseEnough = Vector2.Distance(player.position, ghost.transform.position) < ghost.playerScanDistance;
        bool playerAlive = player.GetComponent<PlayerStats>()?.isDead == false;

        return (playerDetected || playerCloseEnough) && playerAlive;
    }
}
