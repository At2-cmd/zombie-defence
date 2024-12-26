using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [SerializeField] private SamplePoolElement sampleElement;
    public override void InstallBindings()
    {
        Container.BindMemoryPool<SamplePoolElement, SamplePoolElement.Pool>()
                .WithInitialSize(10)
                .ExpandByDoubling()
                .FromComponentInNewPrefab(sampleElement)
                .UnderTransformGroup("SampleElements");
    }
}