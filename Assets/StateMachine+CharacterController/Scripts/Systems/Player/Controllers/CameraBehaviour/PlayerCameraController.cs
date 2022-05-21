using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerCameraController : MonoBehaviour
{
    private CinemachineFramingTransposer _transposer;
    
    [Inject] private readonly OriginRotationModule _originRotation;
    [Inject] private readonly CameraZoomModule _zoom;
    [Inject] private readonly InputReader _input;
    [Inject] private readonly CameraData _cameraData;
    [Inject(Id = IDConstants.VIRTUALCAMERA)] private readonly CinemachineVirtualCamera _virtualCamera;
    
    private void Awake()
    {
        _transposer = _virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body) as CinemachineFramingTransposer;
        _transposer.m_CameraDistance = _cameraData.MaxZoomDistance;
    }

    private void Update()
    {
        if (_input.CameraScroll != 0)
        {
            _transposer.m_CameraDistance = Mathf.Clamp(_transposer.m_CameraDistance + _zoom.CalculateZoomAmount(_input.CameraScroll), _cameraData.MinZoomDistance, _cameraData.MaxZoomDistance);
        }
    }

    private void LateUpdate()
    {
        _originRotation.RotateOrigin();    
    }

}


public class CameraZoomModule
{

    public float CalculateZoomAmount(float delta)
    {
        return 0;
    }
}