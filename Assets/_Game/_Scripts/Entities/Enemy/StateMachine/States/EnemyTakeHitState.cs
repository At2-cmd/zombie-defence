
using UnityEngine;

public class EnemyTakeHitState : EnemyStateBase
{
    private float _takeHitTimer = .5f;
    public override void EnterState(EnemyEntity enemy)
    {
        enemy.EnemyAnimation.PlayAnim(EnemyAnimation.TakeHit);
        enemy.NavMeshAgent.isStopped = true;
    }

    public override void UpdateState(EnemyEntity enemy)
    {
        _takeHitTimer -= Time.deltaTime;
        if (_takeHitTimer <= 0f) enemy.SwitchState(enemy.WalkState);
    }
}
