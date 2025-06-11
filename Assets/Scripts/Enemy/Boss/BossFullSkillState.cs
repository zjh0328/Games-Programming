using System.Collections;
using UnityEngine;

public class BossFullSkillState : EnemyState
{
    private Boss boss;

    public BossFullSkillState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Boss bossRef)
        : base(enemyBase, stateMachine, animBoolName)
    {
        boss = bossRef;
    }

    public override void Enter()
    {
        base.Enter();
        boss.StartCoroutine(ExecuteFullSkill());
    }

    private IEnumerator ExecuteFullSkill()
    {
        yield return new WaitForSeconds(1.2f);
        stateMachine.ChangeState(boss.BattleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
