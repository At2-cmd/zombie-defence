using UnityEngine;
using Zenject;

public class GameManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<GameManager>().FromComponentInHierarchy().AsSingle();
    }
}