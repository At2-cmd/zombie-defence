using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyEntity : MonoBehaviour, IDamageable
{
    [Inject] private IPlayerControllerDataProvider _playerControllerDataProvider;
    [Inject] private IPlayerController _playerController;
    [Inject] private IEnemyController _enemyController;
    [SerializeField] private NavMeshAgent navmeshAgent;
    [SerializeField] private EnemyAnimation enemyAnimation;
    [SerializeField] private Transform modelTransform;

    [Header("Enemy State Machine")]
    private EnemyStateBase _currentState;
    public EnemyIdleState IdleState = new EnemyIdleState();
    public EnemyWalkState WalkState = new EnemyWalkState();
    public EnemyAttackState AttackState= new EnemyAttackState();
    public EnemyTakeHitState TakeHitState= new EnemyTakeHitState();
    public EnemyDieState DieState = new EnemyDieState();

    private Pool _pool;
    private Transform _transform;
    private float _currentEnemyHealth;
    private bool _isDied;
    private const float _defaultInitialEnemyHealth = 3;
    public EnemyAnimation EnemyAnimation => enemyAnimation;
    public NavMeshAgent NavMeshAgent => navmeshAgent;
    public Transform Transform => _transform;
    public Transform PlayerTransform => _playerControllerDataProvider.PlayerTransform;
    public float DistanceToPlayer => (_transform.position - _playerControllerDataProvider.PlayerTransform.position).sqrMagnitude;
    public bool IsDied => _isDied;
    public Action OnEnemyDied;
    private void Initialize()
    {
        _transform = transform;
        enemyAnimation.Initialize();
    }

    private void OnSpawned()
    {
        modelTransform.localScale = Vector3.zero;
        modelTransform.DOScale(Vector3.one, .5f);
        ResetValues();
        _currentState = WalkState;
        _currentState.EnterState(this);
    }

    private void ResetValues()
    {
        modelTransform.localPosition = Vector3.zero;
        _isDied = false;
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

    public void SetEnemyInitialHealth(float initialHealth = _defaultInitialEnemyHealth)
    {
        _currentEnemyHealth = initialHealth;
    }

    private void OnDespawned()
    {

    }

    public void TakeDamage(float damageAmount)
    {
        if (_isDied) return;
        _currentEnemyHealth -= damageAmount;
        SwitchState(TakeHitState);
        if (_currentEnemyHealth <= 0f)
        {
            _isDied = true;
            EventController.Instance.RaiseEnemyKilled();
            SwitchState(DieState);
            OnEnemyDied?.Invoke();
            DOVirtual.DelayedCall(2f, () => 
            {
                modelTransform.DOLocalMoveY(-1f, 1f).OnComplete(Despawn);
            });
        }
    }

    public void DealDamage(float damageAmount)
    {
        _playerController.DealDamageToPlayer(damageAmount);
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
