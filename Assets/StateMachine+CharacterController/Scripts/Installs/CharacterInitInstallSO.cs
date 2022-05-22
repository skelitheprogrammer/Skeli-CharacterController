using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CharacterInitInstallSO", menuName = "Installers/CharacterInitInstallSO")]
public class CharacterInitInstallSO : ScriptableObjectInstaller<CharacterInitInstallSO>
{
	[SerializeField] private GroundCheckDataSO _groundCheck;
	[SerializeField] private GroundCheckDataSO _directionCheck;
	[SerializeField] private PlayerJumpDataSO _jumpData;
	[SerializeField] private PlayerMovementDataSO _movementData;
	[SerializeField] private PlayerRotationDataSO _playerRotationData;
	[SerializeField] private PlayerGravityDataSO _playerGravityData;

	public override void InstallBindings()
	{	
		Container.BindInstance(_groundCheck.Data).WithId(IDConstants.GROUNDCHECK).AsCached().NonLazy();
		Container.BindInstance(_directionCheck.Data).WithId(IDConstants.DIRECTIONCHECK).AsCached().NonLazy();
		Container.BindInstance(_jumpData.Data).AsSingle().NonLazy();
		Container.BindInstance(_movementData.Data).AsSingle().NonLazy();
		Container.BindInstance(_playerRotationData.Data).AsSingle().NonLazy();
		Container.BindInstance(_playerGravityData.Data).AsSingle().NonLazy();

		Container.Bind<StateMachineContext>().AsTransient().NonLazy();
		Container.BindInterfacesAndSelfTo<StateMachineTickable>().AsCached().NonLazy();

		Container.Bind<GroundCheckController>().AsSingle().NonLazy();
		Container.Bind<DirectionController>().AsSingle().NonLazy();

		Container.Bind<PlayerJumpCalculator>().AsSingle().NonLazy();
		Container.Bind<GravitySystem>().AsSingle().NonLazy();

		Container.Bind<FreeFormMovementModule>().AsSingle().NonLazy();
		Container.Bind<StrafeMovementModule>().AsSingle().NonLazy();
		Container.Bind<PlayerMovementController>().AsCached().NonLazy();

		Container.Bind<FreeFormRotationModule>().AsCached().NonLazy();
		Container.Bind<StrafeRotationModule>().AsSingle().NonLazy();
		Container.Bind<PlayerRotationController>().AsCached().NonLazy();

		Container.Bind<CoyoteBufferCalculator>().AsSingle().NonLazy();
		Container.Bind<PlayerJumpController>().AsSingle().NonLazy();
	}
}
