using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerSpawnInstall : MonoInstaller
{

	[SerializeField] private GameObject _playerPrefab;
	[SerializeField] private GameObject _camera;
	[SerializeField] private GameObject _vCam;
	
	public Transform _playerTransform;
	public Transform _rotateOrigin;

	public override void InstallBindings()
	{
		Container.BindFactory<Transform, PlayerSpawner.PlayerFactory>()
		.FromComponentInNewPrefab(_playerPrefab)
		.AsCached()
		.NonLazy();

		Container.BindFactory<Transform, PlayerSpawner.CameraFactory>()
		.FromComponentInNewPrefab(_camera)
		.AsCached()
		.NonLazy();
		
		Container.BindFactory<Transform, PlayerSpawner.VCamFactory>()
		.FromComponentInNewPrefab(_vCam)
		.AsCached()
		.NonLazy();
		
		// Debug.Log("1");
		// Container.Bind<Transform>()
		//     .WithId(Constants.PLAYERTRANSFORM)
		//     .FromComponentInNewPrefab(_prefab)
		//     .AsCached()
		//     .OnInstantiated<Transform>(OnInstant)
		//     .NonLazy();
		// Debug.Log("2");

	}

	private void OnInstant(InjectContext context, Transform transform)
	{
		transform.position = base.transform.position;
		transform.parent = null;
	}
}
