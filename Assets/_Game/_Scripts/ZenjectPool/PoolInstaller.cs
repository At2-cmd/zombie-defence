using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [SerializeField] private EnemyEntity enemyEntity;
    public override void InstallBindings()
    {
        Container.BindMemoryPool<EnemyEntity, EnemyEntity.Pool>()
                .WithInitialSize(10)
                .ExpandByDoubling()
                .FromComponentInNewPrefab(enemyEntity)
                .UnderTransformGroup("EnemyEntities");
    }
}