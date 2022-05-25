using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerCameraController : MonoBehaviour
{
    private Cinemachine3rdPersonFollow _thirdPerson;

    [Inject] private readonly OriginRotationModule _originRotation;
    [Inject] private readonly CameraZoomModule _zoom;
    [Inject] private readonly InputReader _input;
    [Inject] private readonly CameraData _cameraData;
    [Inject(Id = IDConstants.VIRTUALCAMERA)] private readonly CinemachineVirtualCamera _virtualCamera;

    private float _velocity;
    private float _currentZoomValue;

    private void Awake()
    {
        _thirdPerson = _virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        _currentZoomValue = _cameraData.MaxZoomDistance;
        SetCameraDistance(_currentZoomValue);

    }

    private void Update()
    {
        if (_input.CameraScroll != 0)
        {
            _currentZoomValue = Mathf.Clamp(_thirdPerson.CameraDistance + _zoom.CalculateZoomDelta(_input.CameraScroll * _cameraData.ZoomAmount), _cameraData.MinZoomDistance, _cameraData.MaxZoomDistance);
        }
       
        SetCameraDistance(_currentZoomValue);
    }

    private void LateUpdate()
    {
        _originRotation.RotateOrigin(_input.RotateInput);    
    }

    private void SetCameraDistance(float value)
    {     
        _thirdPerson.CameraDistance = Mathf.SmoothDamp(_thirdPerson.CameraDistance, value, ref _velocity, _cameraData.ZoomSmoothTime);
    }

}
