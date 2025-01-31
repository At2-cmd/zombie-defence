using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

public class PlayerShooter : MonoBehaviour
{
    [Inject] private BulletEntity.Pool _bulletPool;
    [Inject] private IPlayerController _playerController;

    [SerializeField] private float shootCooldown = 0.5f;
    [SerializeField] private float enemyDetectionRange = 10f;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform modelTransform;
    [SerializeField] private ParticleSystem muzzleParticle;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private SpriteRenderer detectionRangeVisualizer;

    private BulletEntity _currentBullet;
    private float _shootTimer;
    private EnemyEntity _targetEnemy;
    private float _distanceSqr;
    private Vector3 _shootDirection;
    public Transform PickedTargetTransform => _targetEnemy?.transform;

    public void Initialize(){}

    public void CheckForShoot()
    {
        _shootTimer += Time.deltaTime;

        if (_targetEnemy != null)
        {
            _distanceSqr = (_targetEnemy.transform.position - transform.position).sqrMagnitude;
            if (_distanceSqr > enemyDetectionRange * enemyDetectionRange)
            {
                ResetTarget();
            }
        }

        DetectClosestEnemy();
        TryShoot();
    }

    private void ResetTarget()
    {
        if (_targetEnemy != null)
        {
            _targetEnemy.OnEnemyDied -= HandleEnemyDeath;
            _targetEnemy = null;
            detectionRangeVisualizer.DOColor(Color.white, .1f);
        }
    }

    private void TryShoot()
    {
        if (_shootTimer < shootCooldown || _targetEnemy == null) return;

        _shootTimer = 0f;
        ShootAtTarget();
    }

    private void ShootAtTarget()
    {
        muzzleParticle.Play();
        _shootDirection = ((_targetEnemy.transform.position + (Vector3.up * 1.25f)) - bulletSpawnPoint.position).normalized;
        _currentBullet = _bulletPool.Spawn(bulletSpawnPoint.position);
        _currentBullet.transform.forward = _shootDirection;
        _currentBullet.MoveToTarget(_currentBullet.transform.position + _shootDirection * 20, _currentBullet.Despawn);
    }

    private void DetectClosestEnemy()
    {
        if (_targetEnemy != null) return;

        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, enemyDetectionRange, enemyLayer);
        _targetEnemy = GetClosestEnemy(enemyColliders);

        if (_targetEnemy != null)
        {
            _targetEnemy.OnEnemyDied += HandleEnemyDeath;
            detectionRangeVisualizer.DOColor(Color.red, .1f);
        }
    }

    private EnemyEntity GetClosestEnemy(Collider[] enemies)
    {
        EnemyEntity closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (Collider enemyCollider in enemies)
        {
            EnemyEntity enemy = enemyCollider.GetComponent<EnemyEntity>();
            if (enemy == null || enemy.IsDied) continue;

            float distanceSqr = (enemy.transform.position - transform.position).sqrMagnitude;
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    private void HandleEnemyDeath()
    {
        if (_targetEnemy == null) return;

        _targetEnemy.OnEnemyDied -= HandleEnemyDeath;
        _targetEnemy = null;
        detectionRangeVisualizer.DOColor(Color.white, .1f);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDetectionRange);
    }
#endif
}
