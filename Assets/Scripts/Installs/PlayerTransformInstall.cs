using UnityEngine;
using Zenject;

public class PlayerTransformInstall : MonoInstaller
{
    [SerializeField] private Transform _rotateOrigin;
    [SerializeField] private Transform _playerTransform;

    public override void InstallBindings()
    {
        Container.BindInstance(_playerTransform)
           .WithId(Constants.PLAYERTRANSFORM)
           .AsCached()
           .NonLazy();

        Container.BindInstance(_rotateOrigin)
            .WithId(Constants.ROTATEORIGIN)
            .AsCached()
            .NonLazy();

    }

}