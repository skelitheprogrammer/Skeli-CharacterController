using Cinemachine;
using UnityEngine;
using Zenject;

public class RotateOriginInstall : MonoInstaller
{
    [SerializeField] private Transform _rotateOrigin;

    public override void InstallBindings()
    {
        Container.BindInstance(_rotateOrigin).WithId(Constants.ROTATEORIGIN).AsCached().OnInstantiated<Transform>(OnFooInstantiated).NonLazy();
    }

    void OnFooInstantiated(InjectContext context, Transform foo)
    {
        context.Container.Resolve<CinemachineVirtualCamera>().m_Follow = foo.transform;
    }
}