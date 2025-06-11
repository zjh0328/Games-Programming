using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    [Header("StopDistance")]
    [SerializeField]public float stopApproachDistance = 2.5f;

    #region States
    public SkeletonIdleState IdleState { get; private set; }
    public SkeletonMoveState MoveState { get; private set; }
    public SkeletonBattleState BattleState { get; private set; }
    public SkeletonAttackState AttackState { get; private set; }
    public SkeletonDeathState DeathState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        IdleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        MoveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        BattleState = new SkeletonBattleState(this, stateMachine, "Move", this);
        AttackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        DeathState = new SkeletonDeathState(this, stateMachine, "Idle", this);
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

    public override void AttackTrigger()
    {
        AudioManager.instance.PlaySFX(7, transform);
        base.AttackTrigger();
    }

    protected override void InitializeLastTimeInfo()
    {
        lastTimeAttacked = 0;
    }
}
