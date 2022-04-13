using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerCharacterInstaller", menuName = "Installers/PlayerCharacterInstaller")]
public class PlayerCharacterInstaller : ScriptableObjectInstaller
{
    [SerializeField] private PlayerMovementDataSO _moveData;
    [SerializeField] private PlayerRotationDataSO _rotationData;
    [SerializeField] private PlayerJumpingDataSO _jumpData;
    [SerializeField] private PlayerGravityDataSO _gravityData;

    public override void InstallBindings()
    {
        Container.Bind<PlayerMovementData>().FromInstance(_moveData.Data).AsSingle();
        Container.Bind<PlayerRotationData>().FromInstance(_rotationData.Data).AsSingle();
        Container.Bind<PlayerJumpingData>().FromInstance(_jumpData.Data).AsSingle();
        Container.Bind<PlayerGravityData>().FromInstance(_gravityData.Data).AsSingle();
        Container.Bind<PlayerMovement>().AsSingle().NonLazy();
        Container.Bind<PlayerRotation>().AsSingle().NonLazy();
        Container.Bind<PlayerJumping>().AsSingle().NonLazy();
        Container.Bind<PlayerGravity>().AsSingle().NonLazy();
    }
}
