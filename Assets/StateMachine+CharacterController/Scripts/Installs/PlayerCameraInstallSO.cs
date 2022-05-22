using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Installers/Player Camera Installer")]
public class PlayerCameraInstallSO : ScriptableObjectInstaller
{
    [SerializeField] private CameraDataSO _cameraData;

    public override void InstallBindings()
    {
        Container.BindInstance(_cameraData.Data).AsSingle().NonLazy();

        Container.Bind<OriginRotationModule>().AsSingle().NonLazy();
        Container.Bind<CameraZoomModule>().AsSingle().NonLazy();
    }
}
