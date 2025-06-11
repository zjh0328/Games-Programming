using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBattleState : EnemyState
{
    private Transform player;
    private int moveDirection;
    private Ghost ghost;

    public GhostBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Ghost ghostRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        ghost = ghostRef;
    }

    public override void Enter()
    {
        base.Enter();

        DelayTime = ghost.aggressiveTime;
        player = PlayerManager.instance.player.transform;

        FacePlayer();

        if (player.GetComponent<PlayerStats>().isDead)
        {
            stateMachine.ChangeState(ghost.MoveState);
        }
    }

    public override void Update()
    {
        base.Update();

        if (ghost.stateMachine.CurrentState != this)
            return;

        FacePlayer();

        var detection = ghost.IsPlayerDetected();

        if (detection.collider != null)
        {
            DelayTime = ghost.aggressiveTime;

            if (detection.distance < ghost.attackDistance && CanAttack())
            {
                ChangeToIdleAnimation();
                stateMachine.ChangeState(ghost.AttackState);
                return;
            }
        }
        else
        {
            float distanceToPlayer = Vector2.Distance(player.position, ghost.transform.position);
            if (DelayTime < 0 || distanceToPlayer > ghost.playerScanDistance)
            {
                stateMachine.ChangeState(ghost.IdleState);
                return;
            }
        }

        if (detection.collider != null &&
            Vector2.Distance(ghost.transform.position, player.position) < ghost.attackDistance)
        {
            ChangeToIdleAnimation();
            return;
        }

        moveDirection = player.position.x >= ghost.transform.position.x ? 1 : -1;

        if (ghost.IsAtJumpPoint() && ghost.CanJump())
        {
            ghost.Jump(ghost.battleMoveSpeed * moveDirection, ghost.jumpForce);
            ChangeToMoveAnimation();
            return;
        }

        if (!ghost.IsGroundDetected())
        {
            ghost.SetVelocity(ghost.battleMoveSpeed * moveDirection, rb.velocity.y);
            ChangeToMoveAnimation();
            return;
        }

        ghost.SetVelocity(ghost.battleMoveSpeed * moveDirection, rb.velocity.y);
        ChangeToMoveAnimation();
    }

    private bool CanAttack()
    {
        return Time.time - ghost.lastTimeAttacked >= ghost.attackCooldown && !ghost.isKnockbacked && Mathf.Abs(rb.velocity.y) <= 0.1f;
    }

    private void ChangeToIdleAnimation()
    {
        if (!anim.GetBool("Idle"))
        {
            anim.SetBool("Move", false);
            anim.SetBool("Idle", true);
        }
    }

    private void ChangeToMoveAnimation()
    {
        if (!anim.GetBool("Move"))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Move", true);
        }
    }

    private void FacePlayer()
    {
        float deltaX = player.position.x - ghost.transform.position.x;
        if ((deltaX < 0 && ghost.facingDirection != -1) || (deltaX > 0 && ghost.facingDirection != 1))
        {
            ghost.Flip();
        }
    }
}
