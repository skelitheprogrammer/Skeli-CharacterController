using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerCharacterInstaller", menuName = "Installers/PlayerCharacterInstaller")]
public class PlayerCharacterInstaller : ScriptableObjectInstaller<PlayerCharacterInstaller>
{
    [SerializeField] private PlayerMovementDataSO _moveData;
    [SerializeField] private PlayerRotationDataSO _rotationData;

    public override void InstallBindings()
    {
        Container.Bind<PlayerMovement>().AsSingle().WithArguments(_moveData.Data).NonLazy();
        Container.Bind<PlayerRotation>().AsSingle().WithArguments(_rotationData.Data).NonLazy();
/*        Container.Bind<PlayerJumping>().AsSingle().NonLazy();
        Container.Bind<PlayerGravity>().AsSingle().NonLazy();*/
    }
}
