using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        player.SetZeroVelocity();
        AudioManager.instance.PlaySFX(1, player.transform);
        UI.instance.SwitchToTryAgain();
    }

    public override void Update()
    {
        base.Update();
        player.SetZeroVelocity();
    }
}
