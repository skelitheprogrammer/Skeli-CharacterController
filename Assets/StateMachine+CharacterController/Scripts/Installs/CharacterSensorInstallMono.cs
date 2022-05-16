using UnityEngine;
using Zenject;

public class CharacterSensorInstallMono : MonoInstaller
{
    [SerializeField] private SensorBehaviour _sensorBehaviour;

    public override void InstallBindings()
    {
        Container.BindInstance(_sensorBehaviour.Sensor).WithId(IDConstants.GROUNDCHECK).AsSingle().NonLazy();
    }
}