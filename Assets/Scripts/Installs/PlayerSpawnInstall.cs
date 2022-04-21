using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerSpawnInstall : MonoInstaller
{

	//Сделай фабрику для того чтобы заинжектить все как надо
	[SerializeField] private GameObject _prefab;
	[SerializeField] private GameObject _camera;
	[SerializeField] private GameObject _vCam;

	public override void InstallBindings()
	{
		Debug.Log("1");
        Container.Bind<Transform>()
            .WithId(Constants.PLAYERTRANSFORM)
            .FromComponentInNewPrefab(_prefab)
            .AsCached()
            .OnInstantiated<Transform>(OnInstant)
            .NonLazy();
		Debug.Log("2");

	}

	public override void Start()
	{
        Debug.Log("3");
        Container.InstantiatePrefab(_camera).transform.parent = null;
        Container.InstantiatePrefab(_vCam).transform.parent = null;
        Debug.Log("4");
    }

	private void OnInstant(InjectContext context, Transform transform)
	{
		transform.position = base.transform.position;
		transform.parent = null;
	}
}
