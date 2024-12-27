using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [SerializeField] private EnemyEntity enemyEntity;
    [SerializeField] private BulletEntity bulletEntity;
    public override void InstallBindings()
    {
        Container.BindMemoryPool<EnemyEntity, EnemyEntity.Pool>()
                .WithInitialSize(10)
                .ExpandByDoubling()
                .FromComponentInNewPrefab(enemyEntity)
                .UnderTransformGroup("EnemyEntitiesPool");
        
        Container.BindMemoryPool<BulletEntity, BulletEntity.Pool>()
                .WithInitialSize(20)
                .ExpandByDoubling()
                .FromComponentInNewPrefab(bulletEntity)
                .UnderTransformGroup("BulletEntitiesPool");
    }
}