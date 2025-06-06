using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EntityFX))]
public class Enemy : Entity
{
    [Header("PatrolMove")]
    public float patrolMoveSpeed;
    public float patrolTime;

    [Header("Scan")]
    public float playerScanDistance = 10;
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Battle/Aggressive")]
    public float battleMoveSpeed;
    public float aggressiveTime = 7;

    [Header("Attack")]
    public float attackDistance = 2;
    public float attackCooldown = 1.5f;
    [HideInInspector] public float lastTimeAttacked;

    protected Player player { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }

    private EnemyStats enemyStats;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        enemyStats = GetComponent<EnemyStats>();
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

        switch (level)
        {
            case 1: // Easy
                patrolMoveSpeed = 2f;
                battleMoveSpeed = 2.5f;
                attackCooldown = 2f;
                break;
            case 2: // Normal
                patrolMoveSpeed = 3f;
                battleMoveSpeed = 3.5f;
                attackCooldown = 1.5f;
                break;
            case 3: // Hard
                patrolMoveSpeed = 4f;
                battleMoveSpeed = 4.5f;
                attackCooldown = 1f;
                break;
            default:
                Debug.LogWarning($"Unknown enemy level: {level}, using default values.");
                break;
        }
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.CurrentState.Update();
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

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * attackDistance * facingDirection);
    }

    public virtual void EnterBattleState() { }

    protected virtual void InitializeLastTimeInfo() { }
}
