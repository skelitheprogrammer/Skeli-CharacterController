using FSM;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CharacterInitInstallSO", menuName = "Installers/CharacterInitInstallSO")]
public class CharacterInitInstallSO : ScriptableObjectInstaller<CharacterInitInstallSO>
{
    [SerializeField] private GroundCheckDataSO _groundCheck;
    [SerializeField] private PlayerJumpDataSO _jumpData;
    [SerializeField] private PlayerMovementDataSO _movementData;

    public override void InstallBindings()
    {
        Container.Bind<GroundCheckData>().FromInstance(_groundCheck.Data).AsSingle().NonLazy();
        Container.Bind<PlayerJumpData>().FromInstance(_jumpData.Data).AsSingle().NonLazy();
        Container.Bind<PlayerMovementData>().FromInstance(_movementData.Data).AsSingle().NonLazy();

        Container.Bind<GroundCheckController>().AsSingle().NonLazy();
        Container.Bind<DirectionController>().AsSingle().NonLazy();

        Container.Bind<StateMachine>().AsCached().NonLazy();
        Container.Bind<PlayerSimpleMovementSystem>().AsSingle().NonLazy();
        Container.Bind<PlayerJumpSystem>().AsSingle().NonLazy();
        Container.Bind<GravitySystem>().AsSingle().NonLazy();

        Container.Bind<CharacterStateData>().AsSingle().NonLazy();
    }
}
