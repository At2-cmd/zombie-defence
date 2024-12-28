using System;
using UnityEngine;
using Zenject;

public class PlayerShooter : MonoBehaviour
{
    [Inject] BulletEntity.Pool _bulletEntitiesPool;
    [SerializeField] private float shootCooldown;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private ParticleSystem muzzleParticle;
    private BulletEntity _currentBullet;
    private float _shootTimer;
    public void Initialize() { }

    public void CheckForShoot()
    {
        _shootTimer += Time.deltaTime;
        if (_shootTimer >= shootCooldown)
        {
            _shootTimer = 0f;
            muzzleParticle.Play();
            _currentBullet = _bulletEntitiesPool.Spawn(bulletSpawnPoint.position);
            _currentBullet.transform.forward = bulletSpawnPoint.forward;
            _currentBullet.MoveToTarget((_currentBullet.transform.position + _currentBullet.transform.forward * 20),
                _currentBullet.Despawn);
        }
    }
}