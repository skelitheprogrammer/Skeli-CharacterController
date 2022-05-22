using UnityEngine;
using Zenject;

public class PlayerInstall : MonoInstaller
{
    [SerializeField] private InputReader _input;

    public override void InstallBindings()
    {
        Container.BindInstance(_input).AsSingle().NonLazy();
    }
}