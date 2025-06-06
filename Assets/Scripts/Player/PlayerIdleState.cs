using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.currentState != player.idleState)
            return;

        if (horizontalInput != 0 && !player.isBusy)
        {
            if (!(player.IsWallDetected() && horizontalInput == player.facingDirection))
            {
                stateMachine.ChangeState(player.moveState);
            }
        }
    }
}
