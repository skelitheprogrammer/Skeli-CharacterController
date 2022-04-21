using Cinemachine;
using UnityEngine;
using Zenject;

public class RotateOriginInstall : MonoInstaller
{
    [SerializeField] private Transform _rotateOrigin;

    public override void InstallBindings()
    {
        Container.BindInstance(_rotateOrigin)
            .WithId(Constants.ROTATEORIGIN)
            .AsSingle()
            .NonLazy();
    }
}