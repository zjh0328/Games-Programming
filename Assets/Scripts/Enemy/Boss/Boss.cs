using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    [Header("Full screen skill")]
    [SerializeField] private float FullDamage;

    #region States
    public BossIdleState IdleState { get; private set; }
    public BossMoveState MoveState { get; private set; }
    public BossBattleState BattleState { get; private set; }
    public BossAttackState AttackState { get; private set; }
    public BossDeathState DeathState { get; private set; }
    public BossFullSkillState FullSkillState { get; private set; }
    public BossDoubleAttackState DoubleAttackState { get; private set; }
    #endregion

    private bool fullSkillReleased = false;

    protected override void Awake()
    {
        base.Awake();

        IdleState = new BossIdleState(this, stateMachine, "Idle", this);
        MoveState = new BossMoveState(this, stateMachine, "Move", this);
        BattleState = new BossBattleState(this, stateMachine, "Move", this);
        AttackState = new BossAttackState(this, stateMachine, "Attack", this);
        DeathState = new BossDeathState(this, stateMachine, "Death", this);
        DoubleAttackState = new BossDoubleAttackState(this, stateMachine, "DoubleAttack", this);
        FullSkillState = new BossFullSkillState(this, stateMachine, "FullSkill", this);
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

    public override void AttackTrigger()
    {
        AudioManager.instance.PlaySFX(9, transform);
        base.AttackTrigger();
    }
    public override void TriggerFullSkillDamage()
    {
        AudioManager.instance.PlaySFX(10, transform);
        PlayerStats[] allPlayers = FindObjectsOfType<PlayerStats>();

        foreach (PlayerStats player in allPlayers)
        {
            player.TakeDamage((int)FullDamage, transform, transform);
        }
    }

    public bool CanUseFullSkill()
    {
        return !fullSkillReleased && stats.currentHP <= stats.maxHP * 0.5f;
    }

    public void MarkFullSkillUsed()
    {
        fullSkillReleased = true;
    }
    private void OnDrawGizmosSelected()
    {
        base.OnDrawGizmos();
    }
    
}
