using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerSpawnInstall : MonoInstaller
{
	[SerializeField] private GameObject _prefab;
	[SerializeField] private GameObject _camera;
	[SerializeField] private GameObject _vCam;

	public Transform _rotateObject;

	public override void InstallBindings()
	{		
		Container.Bind<Transform>()
			.WithId(Constants.PLAYERTRANSFORM)
			.FromComponentInNewPrefab(_prefab)
			.AsCached()
			.OnInstantiated<Transform>(OnInstant)
			.NonLazy();
			
		Container.BindInstance(_prefab.transform.GetChild(0))
			.WithId(Constants.ROTATEORIGIN)
			.AsCached()
			.NonLazy();
			

	}

	public override void Start()
	{
	
		Container.InstantiatePrefab(_camera).transform.parent = null;
		
		var vCam = Container.InstantiatePrefabForComponent<CinemachineVirtualCamera>(_vCam);
		vCam.transform.parent = null;
		vCam.Follow = _rotateObject;
	}

	private void OnInstant(InjectContext context, Transform transform)
	{
		transform.position = base.transform.position;
		transform.parent = null;
		
		var child = transform.GetChild(0);
		_rotateObject = child;
;
	}
}
