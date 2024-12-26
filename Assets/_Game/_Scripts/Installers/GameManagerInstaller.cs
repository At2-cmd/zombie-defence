using UnityEngine;
using Zenject;

public class GameManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<EventController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<LevelLoaderController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<EnemyController>().FromComponentInHierarchy().AsSingle();
    }
}