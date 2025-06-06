using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            player.attackCheck.position,
            player.attackCheckRadius,
            LayerMask.GetMask("Enemy") 
        );

        foreach (var hit in colliders)
        {
            EnemyStats targetStats = hit.GetComponent<EnemyStats>();
            if (targetStats != null)
            {
                player.stats.DoDamage(targetStats);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (player == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.attackCheck.position, player.attackCheckRadius);
    }
}
