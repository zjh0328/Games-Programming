using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    [Header("StopDistance")]
    [SerializeField]public float stopApproachDistance = 3.5f;

    [Header("Explosion On Death")]
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionDamage;

    #region States
    public GhostIdleState IdleState { get; private set; }
    public GhostMoveState MoveState { get; private set; }
    public GhostBattleState BattleState { get; private set; }
    public GhostAttackState AttackState { get; private set; }
    public GhostDeathState DeathState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        IdleState = new GhostIdleState(this, stateMachine, "Idle", this);
        MoveState = new GhostMoveState(this, stateMachine, "Move", this);
        BattleState = new GhostBattleState(this, stateMachine, "Move", this);
        AttackState = new GhostAttackState(this, stateMachine, "Attack", this);
        DeathState = new GhostDeathState(this, stateMachine, "Death", this);
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

    public override void EnterBattleState()
    {
        if (stateMachine.CurrentState != BattleState && stateMachine.CurrentState != DeathState)
        {
            stateMachine.ChangeState(BattleState);
        }
    }

    protected override void InitializeLastTimeInfo()
    {
        lastTimeAttacked = 0;
    }

    public override void SpecialAttackTrigger()
    {
        AudioManager.instance.PlaySFX(11, transform);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, whatIsPlayer);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out PlayerStats playerStats))
            {
                playerStats.TakeDamage((int)explosionDamage, transform, transform);
            }
        }
        
        Destroy(gameObject);
    }

    public override void AttackTrigger()
    {
        AudioManager.instance.PlaySFX(5, transform);
        base.AttackTrigger();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
