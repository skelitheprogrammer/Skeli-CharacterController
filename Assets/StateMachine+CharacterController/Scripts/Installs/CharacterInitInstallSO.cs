using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CharacterInitInstallSO", menuName = "Installers/CharacterInitInstallSO")]
public class CharacterInitInstallSO : ScriptableObjectInstaller<CharacterInitInstallSO>
{
	[SerializeField] private GroundCheckDataSO _groundCheck;
	[SerializeField] private PlayerJumpDataSO _jumpData;
	[SerializeField] private PlayerMovementDataSO _movementData;
	[SerializeField] private OriginRotationDataSO _originRotationData;
	[SerializeField] private PlayerRotationDataSO _playerRotationData;
	[SerializeField] private PlayerGravityDataSO _playerGravityData;

	public override void InstallBindings()
	{	
		Container.BindInstance(_groundCheck.Data).AsSingle().NonLazy();
		Container.BindInstance(_jumpData.Data).AsSingle().NonLazy();
		Container.BindInstance(_movementData.Data).AsSingle().NonLazy();
		Container.BindInstance(_originRotationData.Data).AsSingle().NonLazy();
		Container.BindInstance(_playerRotationData.Data).AsSingle().NonLazy();
		Container.BindInstance(_playerGravityData.Data).AsSingle().NonLazy();

		Container.Bind<StateMachineContext>().AsTransient().NonLazy();

		Container.Bind<GroundCheckController>().AsSingle().NonLazy();
		Container.Bind<DirectionController>().AsSingle().NonLazy();

		Container.Bind<PlayerJumpCalculator>().AsSingle().NonLazy();
		Container.Bind<GravitySystem>().AsSingle().NonLazy();

		Container.Bind<PlayerSimpleMovementSystem>().AsSingle().NonLazy();
		Container.Bind<PlayerMovementControllerBase>().To<PlayerMovementController>().AsSingle().NonLazy();

		Container.Bind<PlayerRotationSystem>().AsCached().NonLazy();
		Container.Bind<PlayerRotationControllerBase>().To<PlayerRotationController>().AsSingle().NonLazy();

		Container.Bind<OriginRotationSystem>().AsSingle().NonLazy();
		Container.Bind<CameraControllerBase>().To<PlayerCameraController>().AsSingle().NonLazy();

		Container.Bind<CoyoteBufferCalculator>().AsCached().NonLazy();
		Container.Bind<PlayerJumpControllerBase>().To<PlayerJumpController>().AsSingle().NonLazy();

		Container.Bind<CharacterStateData>().AsSingle().NonLazy();
	}
}
