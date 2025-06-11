using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EntityFX))]
public class Enemy : Entity
{
    [Header("PatrolMove")]
    [SerializeField] public float patrolMoveSpeed;
    public float patrolTime;

    [Header("Scan")]
    public float playerScanDistance = 10;
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Battle/Aggressive")]
    [SerializeField] public float battleMoveSpeed;
    public float aggressiveTime = 7;

    [Header("Attack")]
    [SerializeField] public float attackDistance = 2;
    [SerializeField] public float attackCooldown = 1.5f;
    [HideInInspector] public float lastTimeAttacked;

    [Header("Jump Point")]
    [SerializeField] private LayerMask whatIsJumpPoint;
    [SerializeField] private float jumpPointCheckRadius = 1f;

    [Header("Jump Settings")]
    [SerializeField] public float jumpForce = 10f;
    [SerializeField] private float lastJumpTime = 0f;
    [SerializeField] public float jumpCooldown = 1f;

    [Header("Level Growth Ratios")]
    [SerializeField] private float patrolSpeedGrowthRatio = 0.1f;
    [SerializeField] private float battleSpeedGrowthRatio = 0.1f;
    [SerializeField] private float attackCooldownDecayRatio = 0.1f;
    [SerializeField] private float minAttackCooldown = 0.2f;

    private float basePatrolMoveSpeed;
    private float baseBattleMoveSpeed;
    private float baseAttackCooldown;

    protected Player player { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }
    public bool isJumping { get; private set; } = false;

    private EnemyStats enemyStats;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        enemyStats = GetComponent<EnemyStats>();

        basePatrolMoveSpeed = patrolMoveSpeed;
        baseBattleMoveSpeed = battleMoveSpeed;
        baseAttackCooldown = attackCooldown;
    }

    protected override void Start()
    {
        base.Start();
        player = PlayerManager.instance.player;

        InitializeParametersBasedOnLevel();
    }

    private void InitializeParametersBasedOnLevel()
    {
        int level = enemyStats.GetEnemyLevel();
        float ratio = level - 1;

        patrolMoveSpeed = basePatrolMoveSpeed * (1f + patrolSpeedGrowthRatio * ratio);
        battleMoveSpeed = baseBattleMoveSpeed * (1f + battleSpeedGrowthRatio * ratio);
        attackCooldown = Mathf.Max(minAttackCooldown, baseAttackCooldown * Mathf.Pow(1f - attackCooldownDecayRatio, ratio));
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.CurrentState.Update();

        if (isJumping && IsGroundDetected() && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            isJumping = false;
        }
    }

    public virtual RaycastHit2D IsPlayerDetected()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, playerScanDistance, whatIsPlayer);
    }

    public void AnimationTrigger()
    {
        stateMachine.CurrentState.AnimationFinishTrigger();
    }

    public virtual void SpecialAttackTrigger() { }

    public virtual void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.TryGetComponent(out PlayerStats target))
            {
                stats.DoDamage(target);
            }
        }
    }

    public virtual void TriggerDoubleAttackHit() { }
    public virtual void TriggerFullSkillDamage() { }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * attackDistance * facingDirection);
    }

    public virtual void EnterBattleState() { }

    protected virtual void InitializeLastTimeInfo() { }

    public bool IsAtJumpPoint()
    {
        return Physics2D.OverlapCircle(transform.position, jumpPointCheckRadius, whatIsJumpPoint);
    }

    public bool CanJump()
    {
        return Time.time - lastJumpTime >= jumpCooldown && IsGroundDetected();
    }

    public void Jump(float horizontalForce, float verticalForce)
    {
        lastJumpTime = Time.time;
        isJumping = true;
        rb.velocity = new Vector2(horizontalForce, verticalForce);
    }

    public int GetMoveDirectionToTarget(Transform target)
    {
        return target.position.x >= transform.position.x ? 1 : -1;
    }

}
