using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerSpawnPointFactoryInstaller : LifetimeScope
{
    [SerializeField] private GameObject _prefab;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<PrefabFactory>(Lifetime.Singleton);

        var factory = Container.Resolve<PrefabFactory>();
        var shit = factory.Create();
    }
}
