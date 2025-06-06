using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collision")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance = 1;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance = 0.6f;
    [SerializeField] protected LayerMask whatIsGround;
    [Space]
    public Transform attackCheck;
    public float attackCheckRadius = 1.2f;

    [Header("Knockback")]
    public Vector2 knockbackMovement = new Vector2(5, 3);
    [SerializeField] protected float knockbackDuration = 0.2f;
    public bool isKnockbacked { get; private set; }

    public int facingDirection { get; private set; } = 1;
    public bool facingRight => facingDirection == 1;

    #region Components
    public SpriteRenderer sr { get; private set; }
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public CharacterStats stats { get; private set; }
    public CapsuleCollider2D cd { get; private set; }
    #endregion

    public System.Action onFlipped;

    protected Coroutine knockbackCoroutine;

    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>();
        cd = GetComponent<CapsuleCollider2D>();
    }

    protected virtual void Start() { }

    protected virtual void Update() { }

    public virtual void DamageKnockbackEffect(Transform attacker, Transform attackee)
    {
        if (knockbackCoroutine != null)
            StopCoroutine(knockbackCoroutine);

        float direction = CalculateKnockbackDirection(attacker, attackee);
        knockbackCoroutine = StartCoroutine(ApplyKnockback(direction));
    }

    protected virtual IEnumerator ApplyKnockback(float direction)
    {
        isKnockbacked = true;
        rb.velocity = new Vector2(knockbackMovement.x * direction, knockbackMovement.y);

        yield return new WaitForSeconds(knockbackDuration);

        rb.velocity = new Vector2(0, rb.velocity.y);
        isKnockbacked = false;
    }

    public virtual float CalculateKnockbackDirection(Transform attacker, Transform attackee)
    {
        if (attacker.position.x < attackee.position.x)
            return 1;
        else if (attacker.position.x > attackee.position.x)
            return -1;
        else
            return 0;
    }

    #region Velocity
    public virtual void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnockbacked) return;

        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }

    public virtual void SetZeroVelocity()
    {
        if (isKnockbacked) return;

        rb.velocity = Vector2.zero;
    }
    #endregion

    #region Collision
    protected virtual void OnDrawGizmos()
    {
        if (groundCheck)
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);

        if (wallCheck)
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * wallCheckDistance * facingDirection);

        if (attackCheck)
            Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    public virtual bool IsGroundDetected()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    public virtual bool IsWallDetected()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
    }
    #endregion

    #region Flip
    public virtual void Flip()
    {
        facingDirection = -facingDirection;
        transform.Rotate(0, 180, 0);
        onFlipped?.Invoke();
    }

    public virtual void FlipController(float x)
    {
        if (x > 0 && !facingRight)
            Flip();
        else if (x < 0 && facingRight)
            Flip();
    }
    #endregion

    public virtual void Die()
    {

    }
}
