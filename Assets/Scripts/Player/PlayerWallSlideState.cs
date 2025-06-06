using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.currentState != player.wallSlideState)
            return;

        if (!player.IsWallDetected())
        {
            stateMachine.ChangeState(player.airState);
            return;
        }

        if (verticalInput < 0) 
        {
            player.SetVelocity(0, rb.velocity.y);
        }
        else
        {
            player.SetVelocity(0, rb.velocity.y * 0.2f); 
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if (horizontalInput != 0 && player.facingDirection != horizontalInput)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

}
