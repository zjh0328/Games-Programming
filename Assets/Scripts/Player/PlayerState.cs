using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    private string animBoolName;
    protected float horizontalInput;
    protected float verticalInput;

    protected float stateTimer;
    protected bool useTimer = false;
    protected bool animationFinished = false;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        animationFinished = false;
        useTimer = false;
    }

    public virtual void Update()
    {
        if (useTimer)
            stateTimer -= Time.deltaTime;

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
        animationFinished = false;
        useTimer = false;
    }

    public virtual void AnimationFinishTrigger()
    {
        animationFinished = true;
    }
}
