using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    [Header("Archer Specification")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float ArrowFlySpeed;

    #region States
    public ArcherIdleState IdleState { get; private set; }
    public ArcherMoveState MoveState { get; private set; }
    public ArcherBattleState BattleState { get; private set; }
    public ArcherAttackState AttackState { get; private set; }
    public ArcherDeathState DeathState { get; private set; }
    
    #endregion

    protected override void Awake()
    {
        base.Awake();

        IdleState = new ArcherIdleState(this, stateMachine, "Idle", this);
        MoveState = new ArcherMoveState(this, stateMachine, "Move", this);
        BattleState = new ArcherBattleState(this, stateMachine, "Move", this);
        AttackState = new ArcherAttackState(this, stateMachine, "Attack", this);
        DeathState = new ArcherDeathState(this, stateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();
        InitializeLastTimeInfo();
        stateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(DeathState);
    }

    protected override void InitializeLastTimeInfo()
    {
        lastTimeAttacked = 0;
    }

    public override void EnterBattleState()
    {
        if (stateMachine.CurrentState != BattleState && stateMachine.CurrentState != DeathState)
        {
            stateMachine.ChangeState(BattleState);
        }
    }

    // Archer's special attack is shooting arrow
  public override void SpecialAttackTrigger()
    {
        Vector2 flyDirection = (player.transform.position - transform.position).normalized;

        // Generate point offset to avoid getting stuck on the shooter.
        Vector3 spawnPos = attackCheck.position + (Vector3)(flyDirection * 0.5f);

        GameObject newArrow = Instantiate(arrowPrefab, spawnPos, Quaternion.identity);

        Vector2 finalFlySpeed = flyDirection * ArrowFlySpeed;

        var arrow = newArrow.GetComponent<Arrow_Controller>();
        if (arrow != null)
        {
            arrow.SetupArrow(finalFlySpeed, stats);

            // Actively ignore collisions with the archer.
            Collider2D shooterCol = stats.GetComponent<Collider2D>();
            Collider2D arrowCol = newArrow.GetComponent<Collider2D>();
            if (shooterCol != null && arrowCol != null)
            {
                Physics2D.IgnoreCollision(arrowCol, shooterCol);
            }
        }
    }


    

    public override RaycastHit2D IsPlayerDetected()
    {
        return Physics2D.CircleCast(wallCheck.position, playerScanDistance, Vector2.right * facingDirection, 0, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
    
}
