using UnityEngine;
using Zenject;

public class PlayerInitInstall : MonoInstaller
{
    [SerializeField] private PlayerInitSetupSO _setup;

    public override void InstallBindings()
    {
        Container.Bind<Camera>().WithId(Constants.MainCamera).FromInstance(_setup.MainCamera.GetComponent<Camera>()).NonLazy();
    }

    public override void Start()
    {
        Container.InstantiatePrefab(_setup.Character);
        Container.InstantiatePrefab(_setup.Vcam);
        Container.InstantiatePrefab(_setup.MainCamera);
    }
}