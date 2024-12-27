using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyEntity : MonoBehaviour
{
    [Inject] private IPlayerControllerDataProvider _playerControllerDataProvider;
    [SerializeField] private NavMeshAgent navmeshAgent;
    [SerializeField] private EnemyAnimation enemyAnimation;

    [Header("Enemy State Machine")]
    private EnemyStateBase _currentState;
    public EnemyIdleState IdleState = new EnemyIdleState();
    public EnemyWalkState WalkState = new EnemyWalkState();
    public EnemyAttackState AttackState= new EnemyAttackState();
    public EnemyDieState DieState = new EnemyDieState();

    private Pool _pool;
    private Transform _transform;

    public EnemyAnimation EnemyAnimation => enemyAnimation;
    public NavMeshAgent NavMeshAgent => navmeshAgent;
    public Transform Transform => _transform;
    public Transform PlayerTransform => _playerControllerDataProvider.PlayerTransform;
    public float DistanceToPlayer => (_transform.position - _playerControllerDataProvider.PlayerTransform.position).sqrMagnitude;

    private void Initialize()
    {
        _transform = transform;
        enemyAnimation.Initialize();
    }

    private void OnSpawned()
    {
        _currentState = WalkState;
        _currentState.EnterState(this);
    }
    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(EnemyStateBase newState)
    {
        _currentState = newState;
        _currentState.EnterState(this);
    }

    private void SetPool(Pool pool)
    {
        _pool = pool;
    }

    private void SetPosition(Vector3 position)
    {
        _transform.position = position;
    }

    public void Despawn()
    {
        if (!gameObject.activeSelf) return;
        _pool.Despawn(this);
    }

    private void OnDespawned()
    {
        // Cleanup logic if needed
    }

    public class Pool : MonoMemoryPool<Vector3, EnemyEntity>
    {
        protected override void OnCreated(EnemyEntity item)
        {
            base.OnCreated(item);
            item.SetPool(this);
            item.Initialize();
        }

        protected override void OnDespawned(EnemyEntity item)
        {
            base.OnDespawned(item);
            item.OnDespawned();
        }

        protected override void OnDestroyed(EnemyEntity item)
        {
            base.OnDestroyed(item);
        }

        protected override void Reinitialize(Vector3 position, EnemyEntity item)
        {
            base.Reinitialize(position, item);
            item.SetPosition(position);
            item.OnSpawned();
        }
    }
}
