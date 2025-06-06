using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.Rendering;
using UnityEngine;

public class BlackholeSkillController : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private int damageAmount = 100;
    [SerializeField] private float lifetime = 0.5f; 

    private void Start()
    {
    }
    public void Setup(int damage, float radius)
    {
        this.damageAmount = damage;
        this.radius = radius;
        DealDamageToEnemies();
        Destroy(gameObject, lifetime);
    }


    private void DealDamageToEnemies()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (var hit in hits)
        {
            EnemyStats enemyStats = hit.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(damageAmount, transform, hit.transform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Circular range visualization
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}