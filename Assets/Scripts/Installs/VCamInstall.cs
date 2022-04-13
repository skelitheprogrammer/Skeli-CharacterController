using Cinemachine;
using UnityEngine;
using Zenject;

public class VCamInstall : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vcam;
    [Inject(Id = Constants.MainCamera)] private Camera _camera;

    private void Awake()
    {
        _vcam.Follow = _camera.transform;
    }
}