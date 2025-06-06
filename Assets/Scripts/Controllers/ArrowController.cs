using System.Collections;
using UnityEngine;

public class Arrow_Controller : MonoBehaviour
{
    [SerializeField] private string targetLayerName = "Player";

    private Vector2 flySpeed;
    private Rigidbody2D rb;
    private CharacterStats archerStats;

    [SerializeField] private bool canMove = true;
    [SerializeField] private bool flipped = false;
    private bool isStuck = false;
    private bool destroyScheduled = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(Disappear(10f));
    }

    private void Update()
    {
        if (canMove)
        {
            rb.velocity = flySpeed;
            transform.right = rb.velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
        {
            var target = collision.GetComponent<CharacterStats>();
            if (target != null)
            {
                archerStats.DoDamage(target);
                Insertion(collision);
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Insertion(collision);
        }
    }

    public void FlipArrow()
    {
        if (flipped) return;
        flySpeed.x *= -1;
        flySpeed.y *= -1;
        transform.Rotate(0, 180, 0);
        flipped = true; 
        targetLayerName = "Enemy";
    }

    public void SetupArrow(Vector2 speed, CharacterStats shooterStats)
    {
        flySpeed = speed;

        if (flySpeed.x < 0)
        {
            transform.Rotate(0, 180, 0);
        }

        archerStats = shooterStats;
    }

    private void Insertion(Collider2D collision)
    {
        if (isStuck) return;
        isStuck = true;

        GetComponentInChildren<ParticleSystem>()?.Stop();
        GetComponent<CapsuleCollider2D>().enabled = false;

        canMove = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = collision.transform;

        if (!destroyScheduled)
        {
            destroyScheduled = true;
            StartCoroutine(Disappear(5f));
        }
    }

    private IEnumerator Disappear(float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return FadeAndDestroy();
    }

    private IEnumerator FadeAndDestroy()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float duration = 1.5f;
        float t = 0f;
        Color originalColor = sr.color;

        while (t < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, t / duration);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            t += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
