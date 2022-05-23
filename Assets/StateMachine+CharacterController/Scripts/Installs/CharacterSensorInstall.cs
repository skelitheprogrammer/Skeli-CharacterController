using UnityEngine;
using Zenject;

public class CharacterSensorInstall : MonoInstaller
{
    [SerializeField] private SensorBehaviour _groundCheckSensor;
    [SerializeField] private SensorBehaviour _directionCheckSensor;

    public override void InstallBindings()
    {
        Container.BindInstance(_groundCheckSensor.Sensor).WithId(IDConstants.GROUNDCHECK).AsCached().NonLazy();
        Container.BindInstance(_directionCheckSensor.Sensor).WithId(IDConstants.DIRECTIONCHECK).AsCached().NonLazy();
    }
}