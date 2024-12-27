using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyStateBase
{
    private float _attackTimer;
    private float _attackCooldown = 2f; 
    private float _attackRange = 2.0f;
    public override void EnterState(EnemyEntity enemy)
    {
        enemy.EnemyAnimation.PlayAnim(EnemyAnimation.Attack);
        enemy.NavMeshAgent.isStopped = true;
        _attackTimer = 0f;
    }
    public override void UpdateState(EnemyEntity enemy)
    {
        if(enemy.DistanceToPlayer <= _attackRange)
        {
            if (_attackTimer >= _attackCooldown)
            {
                DealDamageToPlayer();
                _attackTimer = 0f;
            }
            _attackTimer += Time.deltaTime;
        }
        else
        {
            enemy.SwitchState(enemy.WalkState);
        }
    }
    private void DealDamageToPlayer()
    {
        Debug.Log("Enemy Dealed Damage to Player");
    }
}
