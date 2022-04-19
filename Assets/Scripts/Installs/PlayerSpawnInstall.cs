using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerSpawnInstall : MonoInstaller
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _vcam;

    public override void InstallBindings()
    {
        Container.Bind<Transform>().WithId(Constants.PLAYERTRANSFORM).FromComponentInNewPrefab(_prefab).AsCached().OnInstantiated<Transform>(OnInstant).NonLazy();
    }

    public override void Start()
    {
        Container.InstantiatePrefab(_camera).transform.parent = null;
        Container.InstantiatePrefab(_vcam).transform.parent = null;


    }

    public void OnInstant(InjectContext context, Transform transform)
    {
        transform.position = base.transform.position;
        transform.parent = null;
    }

    public void OnInstants(InjectContext context, CinemachineVirtualCamera vcam)
    {
        Debug.Log($"{context}");
    }
}
