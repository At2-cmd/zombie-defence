using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyStateBase
{
    public override void EnterState(EnemyEntity enemy)
    {
        enemy.EnemyAnimation.PlayAnim(EnemyAnimation.Idle);
    }
}
