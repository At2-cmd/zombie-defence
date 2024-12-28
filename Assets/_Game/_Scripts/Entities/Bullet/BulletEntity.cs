using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

public class BulletEntity : MonoBehaviour
{
    [Inject] BulletHitParticle.Pool _bulletHitParticlesPool;
    [SerializeField] private float bulletSpeed;
    private Tweener _bulletMoveTween;
    private Pool _pool;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyEntity enemy))
        {
            if (enemy.IsDied) return;
            _bulletHitParticlesPool.Spawn(transform.position);
            _bulletMoveTween?.Kill();
            enemy.TakeDamage(1);
            Despawn();
        }
    }

    public void Despawn()
    {
        if (!gameObject.activeSelf) return;
        _pool.Despawn(this);
    }

    private void SetPool(Pool pool)
    {
        _pool = pool;
    }
    public void MoveToTarget(Vector3 targetPos, Action onBulletMovementFinishedCallback = null)
    {
        _bulletMoveTween = transform.DOMove(targetPos, bulletSpeed).SetEase(Ease.Linear).SetSpeedBased(true).OnComplete(() =>
        {
            onBulletMovementFinishedCallback?.Invoke();
        });
    }

    private void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    private void OnSpawned()
    {

    }

    public class Pool : MonoMemoryPool<Vector3, BulletEntity>
    {
        protected override void OnCreated(BulletEntity item)
        {
            base.OnCreated(item);
            item.SetPool(this);
        }

        protected override void Reinitialize(Vector3 position, BulletEntity item)
        {
            base.Reinitialize(position, item);
            item.SetPosition(position);
            item.OnSpawned();
        }
    }
}
