using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private int moveDirection;
    private Skeleton skeleton;

    public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Skeleton skeletonRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        skeleton = skeletonRef;
    }

    public override void Enter()
    {
        base.Enter();

        DelayTime = skeleton.aggressiveTime;
        player = PlayerManager.instance.player.transform;

        FacePlayer();

        if (player.GetComponent<PlayerStats>().isDead)
        {
            stateMachine.ChangeState(skeleton.MoveState);
        }
    }

    public override void Update()
    {
        if (skeleton.isJumping)
        {
            skeleton.SetVelocity(skeleton.battleMoveSpeed * skeleton.facingDirection, rb.velocity.y);
            ChangeToMoveAnimation();
            return;
        }

        base.Update();

        if (skeleton.stateMachine.CurrentState != this)
        return;

        float distanceToPlayer = Vector2.Distance(player.position, skeleton.transform.position);

        if (distanceToPlayer <= skeleton.attackDistance && CanAttack())
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Move", false);
            stateMachine.ChangeState(skeleton.AttackState);
            return;
        }

        FacePlayer();

        var detection = skeleton.IsPlayerDetected();

        if (detection.collider != null)
        {
            DelayTime = skeleton.aggressiveTime;

            if ((detection.distance < skeleton.attackDistance) && CanAttack())
            {
                anim.SetBool("Idle", false);
                anim.SetBool("Move", false);
                stateMachine.ChangeState(skeleton.AttackState);
                return;
            }
        }
        else
        {
            float distance = Vector2.Distance(player.transform.position, skeleton.transform.position);
            if (!skeleton.isJumping && (DelayTime < 0 || distance > skeleton.playerScanDistance))
            {
                stateMachine.ChangeState(skeleton.IdleState);
                return;
            }
        }

        if (detection.collider != null &&
            Vector2.Distance(skeleton.transform.position, player.transform.position) < skeleton.attackDistance)
        {
            ChangeToIdleAnimation();
            return;
        }

        moveDirection = skeleton.GetMoveDirectionToTarget(player);

        if (skeleton.IsAtJumpPoint() && skeleton.CanJump())
        {
            skeleton.Jump(skeleton.battleMoveSpeed * moveDirection, skeleton.jumpForce);
            ChangeToMoveAnimation();
            return;
        }

        if (!skeleton.IsGroundDetected())
        {
            return;
        }

        float distanceToPlayerX = Mathf.Abs(player.position.x - skeleton.transform.position.x);

        if (!skeleton.isJumping && skeleton.IsGroundDetected())
        {
            if (distanceToPlayerX > skeleton.stopApproachDistance)
            {
                skeleton.SetVelocity(skeleton.battleMoveSpeed * moveDirection, rb.velocity.y);
                ChangeToMoveAnimation();
            }
            else
            {
                skeleton.SetVelocity(0, rb.velocity.y); 
                ChangeToIdleAnimation(); 
            }
        }
    }



    private bool CanAttack()
    {
        return Time.time - skeleton.lastTimeAttacked >= skeleton.attackCooldown && !skeleton.isKnockbacked && Mathf.Abs(rb.velocity.y) <= 0.1f;
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
        float deltaX = player.position.x - skeleton.transform.position.x;
        if ((deltaX < 0 && skeleton.facingDirection != -1) || (deltaX > 0 && skeleton.facingDirection != 1))
        {
            skeleton.Flip();
        }
    }
}
