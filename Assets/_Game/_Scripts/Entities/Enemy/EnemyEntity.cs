using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyEntity : MonoBehaviour
{
    [Inject] IPlayerControllerDataProvider _playerControllerDataProvider;
    [SerializeField] private NavMeshAgent navmeshAgent;
    [SerializeField] private EnemyAnimation enemyAnimation;
    private Pool _pool;
    private void Initialize()
    {
        enemyAnimation.Initialize();
    }
    private void OnSpawned()
    {
        enemyAnimation.PlayAnim(EnemyAnimation.Walk);
    }
    private void OnDespawned()
    {

    }
    private void Update()
    {
        if (_playerControllerDataProvider.PlayerTransform != null)
        {
            navmeshAgent.SetDestination(_playerControllerDataProvider.PlayerTransform.position);
        }
    }
    private void SetPool(Pool pool)
    {
        _pool = pool;
    }

    private void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    public void Despawn()
    {
        if (!gameObject.activeSelf) return;
        _pool.Despawn(this);
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
