using System;
using UnityEngine;
using Zenject;

public class PlayerShooter : MonoBehaviour
{
    [Inject] private BulletEntity.Pool _bulletEntitiesPool;
    [Inject] private IPlayerController _playerController;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float enemyDetectionRange;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private ParticleSystem muzzleParticle;
    [SerializeField] private LayerMask enemyLayer;

    private BulletEntity _currentBullet;
    private float _shootTimer;
    private EnemyEntity _pickedEnemyEntity;

    public void Initialize() { }

    public void CheckForShoot()
    {
        _shootTimer += Time.deltaTime;
        DetectClosestEnemy();
        if (_shootTimer >= shootCooldown && _pickedEnemyEntity != null)
        {
            _shootTimer = 0f;
            muzzleParticle.Play();
            _currentBullet = _bulletEntitiesPool.Spawn(bulletSpawnPoint.position);
            Vector3 directionToEnemy = (_pickedEnemyEntity.transform.position - bulletSpawnPoint.position).normalized;
            _currentBullet.transform.forward = directionToEnemy;
            _currentBullet.MoveToTarget(_currentBullet.transform.position + directionToEnemy * 20, _currentBullet.Despawn);
        }
    }

    public void DetectClosestEnemy()
    {
        if (_pickedEnemyEntity != null) return;

        Collider[] enemies = Physics.OverlapSphere(transform.position, enemyDetectionRange, enemyLayer);
        float closestDistance = Mathf.Infinity;
        EnemyEntity newTarget = null;

        foreach (Collider enemy in enemies)
        {
            EnemyEntity enemyEntity = enemy.GetComponent<EnemyEntity>();
            if (enemyEntity == null || enemyEntity.IsDied) continue;

            float distance = (enemy.transform.position - transform.position).sqrMagnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                newTarget = enemyEntity;
            }
        }

        if (newTarget != null)
        {
            if (_pickedEnemyEntity != null)
            {
                _pickedEnemyEntity.OnEnemyDied -= OnPickedEnemyDiedHandler;
            }
            _pickedEnemyEntity = newTarget;
            _pickedEnemyEntity.OnEnemyDied += OnPickedEnemyDiedHandler;
        }
    }

    private void OnPickedEnemyDiedHandler()
    {
        if (_pickedEnemyEntity != null)
        {
            _pickedEnemyEntity.OnEnemyDied -= OnPickedEnemyDiedHandler;
            _pickedEnemyEntity = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDetectionRange);
    }
}
