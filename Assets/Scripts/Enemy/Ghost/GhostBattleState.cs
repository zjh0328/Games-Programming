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
        if (ghost.isJumping)
        {
            ghost.SetVelocity(ghost.battleMoveSpeed * ghost.facingDirection, rb.velocity.y);
            ChangeToMoveAnimation();
            return;
        }

        base.Update();

        float distanceToPlayer = Vector2.Distance(player.position, ghost.transform.position);

        if (distanceToPlayer <= ghost.attackDistance && CanAttack())
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Move", false);
            stateMachine.ChangeState(ghost.AttackState);
            return;
        }

        if (ghost.stateMachine.CurrentState != this)
            return;

        FacePlayer();

        var detection = ghost.IsPlayerDetected();

        if (detection.collider != null)
        {
            DelayTime = ghost.aggressiveTime;

            if (detection.distance < ghost.attackDistance && CanAttack())
            {
                anim.SetBool("Idle", false);
                anim.SetBool("Move", false);
                stateMachine.ChangeState(ghost.AttackState);
                return;
            }
        }
        else
        {
            float distance = Vector2.Distance(player.position, ghost.transform.position);
            if (!ghost.isJumping && (DelayTime < 0 || distance > ghost.playerScanDistance))
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

        moveDirection = ghost.GetMoveDirectionToTarget(player);

        if (ghost.IsAtJumpPoint() && ghost.CanJump())
        {
            ghost.Jump(ghost.battleMoveSpeed * moveDirection, ghost.jumpForce);
            ChangeToMoveAnimation();
            return;
        }

        if (!ghost.IsGroundDetected())
        {
            return;
        }

        float distanceToPlayerX = Mathf.Abs(player.position.x - ghost.transform.position.x);

        if (!ghost.isJumping && ghost.IsGroundDetected())
        {
            if (distanceToPlayerX > ghost.stopApproachDistance)
            {
                ghost.SetVelocity(ghost.battleMoveSpeed * moveDirection, rb.velocity.y);
                ChangeToMoveAnimation();
            }
            else
            {
                ghost.SetVelocity(0, rb.velocity.y); 
                ChangeToIdleAnimation();
            }
        }
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
