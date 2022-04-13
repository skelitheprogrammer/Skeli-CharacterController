using UnityEngine;
using Zenject;

public class GameDetectionInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GroundDetection>().AsCached();
        Container.Bind<DirectionController>().AsCached();
    }
}