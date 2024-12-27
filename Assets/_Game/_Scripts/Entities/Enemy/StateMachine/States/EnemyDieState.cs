public class EnemyDieState : EnemyStateBase
{
    public override void EnterState(EnemyEntity enemy)
    {
        enemy.EnemyAnimation.PlayAnim(EnemyAnimation.Die);
        enemy.NavMeshAgent.ResetPath();
        enemy.NavMeshAgent.isStopped = true;
    }
}
