using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Update()
    {
        base.Update();

        bool isGrounded = player.IsGroundDetected();

        if (!isGrounded)
        {
            stateMachine.ChangeState(player.airState);
            return;
        }

        if (isGrounded && player.isOnPlatform)
        {
            if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
            {
                player.JumpOffPlatform();
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.attackState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }
    }
}
