using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
        useTimer = true; 

        player.stats.BecomeInvincible(true);
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y);
        player.stats.BecomeInvincible(false);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.currentState != player.dashState)
            return;

        if (!player.IsGroundDetected() && player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
            return;
        }

        player.SetVelocity(player.dashSpeed * player.dashDirection, 0);

        if (stateTimer <= 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
