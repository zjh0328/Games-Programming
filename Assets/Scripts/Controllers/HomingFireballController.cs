using UnityEngine;

public class HomingFireballController : MonoBehaviour
{
    private Transform target;
    private float speed;
    private float damage;
    private float lifetime;

    [SerializeField] private float rotateSpeed = 720f;

    public void Setup(Transform targetTransform, float moveSpeed, float fireballDamage, float timeToLive)
    {
        target = targetTransform;
        speed = moveSpeed;
        damage = fireballDamage;
        lifetime = timeToLive;

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyStats enemy))
        {
            enemy.TakeDamage((int)damage, transform, other.transform);
            Destroy(gameObject);
        }
    }
}
