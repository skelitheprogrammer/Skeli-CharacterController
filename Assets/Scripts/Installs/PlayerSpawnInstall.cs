using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerSpawnInstall : MonoInstaller
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _vcam;

    private Transform _rotateObject;

    public override void InstallBindings()
    {
        Container.Bind<Transform>()
            .WithId(Constants.PLAYERTRANSFORM)
            .FromComponentInNewPrefab(_prefab)
            .AsCached()
            .OnInstantiated<Transform>(OnInstant)
            .NonLazy();

        Container.BindInstance(_rotateObject)
            .WithId(Constants.ROTATEORIGIN)
            .AsCached()
            .NonLazy();
    }

    public override void Start()
    {
        Container.InstantiatePrefab(_camera).transform.parent = null;
        var vcam = Container.InstantiatePrefabForComponent<CinemachineVirtualCamera>(_vcam);
        vcam.transform.parent = null;
        vcam.Follow = _rotateObject;
    }

    private void OnInstant(InjectContext context, Transform transform)
    {
        transform.position = base.transform.position;
        transform.parent = null;
        _rotateObject = transform.GetComponent<TransformReference>().Transform;
    }
}
