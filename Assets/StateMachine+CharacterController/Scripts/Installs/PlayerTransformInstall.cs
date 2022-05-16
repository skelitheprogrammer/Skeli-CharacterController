using UnityEngine;
using Zenject;

public class PlayerTransformInstall : MonoInstaller
{
    [SerializeField] private Transform _rotateOrigin;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _camera;

    public override void InstallBindings()
    {
        Container.BindInstance(_playerTransform)
           .WithId(IDConstants.PLAYERTRANSFORM)
           .AsCached()
           .NonLazy();

        Container.BindInstance(_rotateOrigin)
            .WithId(IDConstants.ROTATEORIGIN)
            .AsCached()
            .NonLazy();

        Container.BindInstance(_camera)
            .WithId(IDConstants.MAINCAMERA)
            .AsCached()
            .NonLazy();
    }

}