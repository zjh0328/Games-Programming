using System.Collections;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private float lastTimeAttacked;
    private float attackDuration = 0.1f;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(3, player.transform);

        lastTimeAttacked = Time.time;

        horizontalInput = Input.GetAxisRaw("Horizontal");
        float attackDirection = player.facingDirection;
        if (horizontalInput != 0)
            attackDirection = horizontalInput;

        stateTimer = attackDuration;
        useTimer = true; 
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine(player.BusyFor(0.1f));

        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (useTimer && stateTimer < 0)
        {
            player.SetZeroVelocity();
        }

        if (animationFinished)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
