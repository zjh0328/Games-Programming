using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.currentState != player.airState)
            return;

        if (horizontalInput != 0)
        {
            float airSpeed = player.moveSpeed * 0.8f; 
            player.SetVelocity(airSpeed * horizontalInput, rb.velocity.y);
        }

        if (player.IsWallDetected() && !player.isOnPlatform)
        {
            stateMachine.ChangeState(player.wallSlideState);
            return;
        }

        if (player.IsGroundDetected())
        {
            if (horizontalInput != 0)
                stateMachine.ChangeState(player.moveState);
            else
                stateMachine.ChangeState(player.idleState);
        }
    }
}
