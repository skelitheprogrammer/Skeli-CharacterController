using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 2;
    [SerializeField] private float _topClamp = 45;
    [SerializeField] private float _botClamp = -35;
    [Inject] private Transform _cameraRotationOrigin;

    [Inject] private InputReader _input;

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    private const float _threshold = 0.01f;

    private void Awake()
    {
        _cinemachineTargetYaw = transform.localEulerAngles.y;
        _cinemachineTargetPitch = transform.localEulerAngles.x;
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        if (_input.RotateInput.sqrMagnitude >= _threshold)
        {
            _cinemachineTargetYaw += _input.RotateInput.x * _sensitivity * Time.deltaTime;
            _cinemachineTargetPitch += _input.RotateInput.y * _sensitivity * Time.deltaTime;
        }

        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _botClamp, _topClamp);

        _cameraRotationOrigin.transform.rotation = Quaternion.Euler(-_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
