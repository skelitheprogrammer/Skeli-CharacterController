using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerInitInstall : MonoInstaller
{
    [SerializeField] private PlayerInitSetupSO _setup;

    public override void InstallBindings()
    {
        Container.Bind<Camera>().WithId(Constants.MainCamera).FromComponentInNewPrefab(_setup.MainCamera).AsCached();
    }

    public override void Start()
    {
        var vcam = Container.InstantiatePrefab(_setup.Vcam).GetComponent<CinemachineVirtualCamera>();
        var rotateOrigin = Container.InstantiatePrefab(_setup.Character).transform.GetChild(0);

        vcam.Follow = rotateOrigin;
    }
}