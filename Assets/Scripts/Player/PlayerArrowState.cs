using System.Collections;
using UnityEngine;

public class PlayerArrowState : PlayerState
{
    private float arrowSpeed = 10f;
    private float returnDelay = 0.2f;

    public PlayerArrowState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(6, player.transform);

        Vector3 spawnPos = player.attackCheck.position;
        GameObject newArrow = GameObject.Instantiate(player.arrowPrefab, spawnPos, Quaternion.identity);

        Vector2 flyDir = new Vector2(player.facingDirection, 0).normalized;
        Vector2 velocity = flyDir * arrowSpeed;

        newArrow.GetComponent<Arrow_Controller>()?.SetupArrow(velocity, player.stats);

        stateTimer = returnDelay;
        useTimer = true;
    }

    public override void Update()
    {
        base.Update();

        if (useTimer && stateTimer <= 0)
        {
            if (player.IsGroundDetected())
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.airState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
