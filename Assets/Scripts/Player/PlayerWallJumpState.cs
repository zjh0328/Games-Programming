using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        useTimer = true;
        stateTimer = player.wallJumpDuration;

        player.SetVelocity(player.wallJumpSpeed * -player.facingDirection, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.currentState != player.wallJumpState)
            return;

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        if (stateTimer <= 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
