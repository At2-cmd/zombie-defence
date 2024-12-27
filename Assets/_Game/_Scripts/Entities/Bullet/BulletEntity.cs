using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

public class BulletEntity : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private Pool _pool;

    public void Despawn()
    {
        _pool.Despawn(this);
    }

    private void SetPool(Pool pool)
    {
        _pool = pool;
    }
    public void MoveToTarget(Vector3 targetPos, Action onBulletMovementFinishedCallback = null)
    {
        transform.DOMove(targetPos, bulletSpeed).SetSpeedBased(true).OnComplete(() =>
        {
            onBulletMovementFinishedCallback?.Invoke();
        });
    }
    private void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public class Pool : MonoMemoryPool<Vector3, BulletEntity>
    {
        protected override void OnCreated(BulletEntity item)
        {
            base.OnCreated(item);
            item.SetPool(this);
        }

        protected override void OnDespawned(BulletEntity item)
        {
            base.OnDespawned(item);
        }

        protected override void OnDestroyed(BulletEntity item)
        {
            base.OnDestroyed(item);
        }

        protected override void OnSpawned(BulletEntity item)
        {
            base.OnSpawned(item);
        }

        protected override void Reinitialize(Vector3 position, BulletEntity item)
        {
            base.Reinitialize(position, item);
            item.SetPosition(position);
        }
    }
}
