using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkState : EnemyStateBase
{
    private float _attackRange = 2.0f;
    public override void EnterState(EnemyEntity enemy)
    {
        enemy.EnemyAnimation.PlayAnim(EnemyAnimation.Walk);
        enemy.NavMeshAgent.isStopped = false;
    }
    public override void UpdateState(EnemyEntity enemy)
    {
        if (enemy.PlayerTransform == null)
        {
            Debug.Log("Player transform cannot be found.");
            enemy.SwitchState(enemy.IdleState);
            return;
        }

        enemy.NavMeshAgent.SetDestination(enemy.PlayerTransform.position);

        if (enemy.DistanceToPlayer <= _attackRange)
        {
            enemy.SwitchState(enemy.AttackState);
        }
    }
}
