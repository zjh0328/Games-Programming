using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(2, player.transform);
        float horizontalSpeed = rb.velocity.x;
        player.SetVelocity(horizontalSpeed, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.currentState != player.jumpState) return;

        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
