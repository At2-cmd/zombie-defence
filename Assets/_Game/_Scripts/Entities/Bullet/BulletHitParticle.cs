using UnityEngine;
using Zenject;
public class BulletHitParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem bulletHitParticle;
    private Pool _pool;

    private void Initialize()
    {
        var main = bulletHitParticle.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void Play(Vector3 pos)
    {
        transform.position = pos;
        bulletHitParticle.Play();
    }

    private void Despawn()
    {
        if (!gameObject.activeSelf) return;
        _pool.Despawn(this);
    }

    private void SetPool(Pool pool)
    {
        _pool = pool;
    }

    private void OnParticleSystemStopped()
    {
        Despawn();
    }

    public class Pool : MonoMemoryPool<Vector3, BulletHitParticle>
    {
        protected override void OnCreated(BulletHitParticle item)
        {
            base.OnCreated(item);
            item.SetPool(this);
            item.Initialize();
        }

        protected override void Reinitialize(Vector3 pos, BulletHitParticle item)
        {
            base.Reinitialize(pos, item);
            item.Play(pos);
        }
    }
}