using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public SkillManager skill { get; private set; }

    [HideInInspector] public float lastArrowShootTime;

    [Header("Defend Cooldown")]
    public float defendCooldown = 1f;
    [HideInInspector] public float lastDefendTime;

    [Header("Attack Cooldown")]
    public float attackCooldown = 0.1f;
    [HideInInspector] public float lastAttackTime;

    [Header("Arrow Cooldown")]
    public float arrowCooldown = 2f; 

    [Header("Move")]
    public float moveSpeed;
    public float jumpForce;
    public float wallJumpSpeed;
    public float wallJumpDuration;
    private float defaultMoveSpeed;
    private float defaultJumpForce;

    [Header("Dash")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDirection { get; private set; }
    private float defaultDashSpeed;

    [Header("Environment")]
    [SerializeField] private BoxCollider2D downablePlatformCheck;
    public GameObject arrowPrefab;
    public DownablePlatform lastPlatform { get; set; }
    public bool isOnPlatform { get; set; } = false;
    public bool isBusy { get; private set; }
    public PlayerFX fx { get; private set; }

    #region States and Statemachine
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerDeathState deathState { get; private set; }
    public PlayerDefendState defendState { get; private set; }
    public PlayerArrowState arrowState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        fx = GetComponent<PlayerFX>();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        deathState = new PlayerDeathState(this, stateMachine, "Death");
        defendState = new PlayerDefendState(this, stateMachine, "Defend");
        arrowState = new PlayerArrowState(this, stateMachine, "Arrow");
    }

    protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance;
        stateMachine.Initialize(idleState);
        CacheDefaultValues();
    }

    private void CacheDefaultValues()
    {
        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
        defaultDashSpeed = dashSpeed;
    }

    protected override void Update()
    {
        if (Time.timeScale == 0) return;

        base.Update();

        stateMachine.currentState.Update();

        if (stats.isDead) return;

        CheckForDashInput();
        CheckSkillInputs();
    }

    private void CheckSkillInputs()
    {
        if (Input.GetKeyDown(KeyCode.F))
            skill.Fireball.AvailableSkill();

        if (Input.GetKeyDown(KeyCode.R))
            skill.Blackhole.AvailableSkill();

        if (Input.GetKeyDown(KeyCode.M) && Time.time >= lastArrowShootTime + arrowCooldown)
        {
            stateMachine.ChangeState(arrowState);
            lastArrowShootTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.N) && Time.time >= lastDefendTime + defendCooldown)
        {
            stateMachine.ChangeState(defendState);
            lastDefendTime = Time.time;
        }
    }

    private void CheckForDashInput()
    {
        if (IsWallDetected()) return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.Dash.AvailableSkill())
        {
            if (Input.GetKey(KeyCode.A)) dashDirection = -1;
            else if (Input.GetKey(KeyCode.D)) dashDirection = 1;
            else dashDirection = facingDirection;
            stateMachine.ChangeState(dashState);
        }
    }

    public IEnumerator BusyFor(float seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(seconds);
        isBusy = false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
    }

    public void JumpOffPlatform()
    {
        if (isOnPlatform && lastPlatform != null)
        {
            lastPlatform.TemporarilyDisableCollider(0.5f);
        }
    }

    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }
}