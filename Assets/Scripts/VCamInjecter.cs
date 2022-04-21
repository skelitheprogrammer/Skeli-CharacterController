using Cinemachine;
using UnityEngine;
using Zenject;

public class VCamInjecter : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vcam;
    [Inject(Id = Constants.ROTATEORIGIN)] private Transform _rotateOrigin;

    private void Start()
    {
        _vcam.Follow = _rotateOrigin;
    }
}
