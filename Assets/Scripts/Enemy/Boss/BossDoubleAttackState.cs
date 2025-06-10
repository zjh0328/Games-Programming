using System.Collections;
using UnityEngine;

public class BossDoubleAttackState : EnemyState
{
    private Boss boss;

    public BossDoubleAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Boss bossRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        boss = bossRef;
    }

    public override void Enter()
    {
        base.Enter();
        boss.StartCoroutine(PerformDoubleAttack());
    }

    private IEnumerator PerformDoubleAttack()
    {
        yield return new WaitForSeconds(1.2f);
        stateMachine.ChangeState(boss.BattleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
