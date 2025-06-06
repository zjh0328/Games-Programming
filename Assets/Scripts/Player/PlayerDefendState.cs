using UnityEngine;

public class PlayerDefendState : PlayerState
{
    public PlayerDefendState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 1f;
        useTimer = true;

        player.stats.BecomeInvincible(true); 
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        player.stats.BecomeInvincible(false); 
    }


    public override void Update()
    {
        base.Update();

        if (stateMachine.currentState != player.defendState)
            return;

        if (stateTimer <= 0f)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}