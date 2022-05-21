using UnityEngine;
using Zenject;
using Cinemachine;

public class PlayerTransformInstall : MonoInstaller
{
    [SerializeField] private Transform _rotateOrigin;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _camera;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    public override void InstallBindings()
    {
        Container.BindInstance(_playerTransform)
           .WithId(IDConstants.PLAYERTRANSFORM)
           .AsSingle()
           .NonLazy();

        Container.BindInstance(_rotateOrigin)
            .WithId(IDConstants.ROTATEORIGIN)
            .AsSingle()
            .NonLazy();

        Container.BindInstance(_camera)
            .WithId(IDConstants.MAINCAMERA)
            .AsSingle()
            .NonLazy();

        Container.BindInstance(_virtualCamera)
            .WithId(IDConstants.VIRTUALCAMERA)
            .AsSingle()
            .NonLazy();

        Container.BindInstance(_virtualCamera.transform)
            .WithId(IDConstants.VIRTUALCAMERA)
            .AsSingle()
            .NonLazy();
    }

}