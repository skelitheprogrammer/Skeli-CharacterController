using UnityEngine;
using Zenject;

public class PlayerSpawnInstall : MonoInstaller
{
    [SerializeField] private GameObject _prefab;

    public override void InstallBindings()
    {
        Container.Bind<Transform>().WithId(Constants.PLAYERTRANSFORM).FromComponentInNewPrefab(_prefab).AsSingle().OnInstantiated<Transform>(OnInstant).NonLazy();
    }

    public override void Start()
    {

    }

    public void OnInstant(InjectContext context, Transform go)
    {
        go.position = transform.position;
        go.parent = null;
    }
}
