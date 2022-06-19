using UnityEngine;
using UnityEngine.Animations.Rigging;

public class BodyTiltRigController : MonoBehaviour
{
    private InputReader _input;
    [SerializeField] private OverrideTransform _overrideComponent;

    [Range(0,1f)]
    [SerializeField] private float _rotationSmoothTime = 0.2f;
    [Range(0,1f)]
    [SerializeField] private float _roataionFactor;
    [SerializeField] private float _maxRotationAngle = 15f;
    
    private Transform _target;
    
    private float _currentRotation;
    private float _targetRotation;
    
    private float _velocity;

    private void Awake()
    {
        _target = _overrideComponent.data.sourceObject;
    }

    private void Update()
    {
        if (_input.MoveInput != Vector2.zero) 
        {
            _currentRotation = _input.RotateInput.x * _roataionFactor;
            _currentRotation = Mathf.Clamp(_currentRotation, -_maxRotationAngle, _maxRotationAngle);

        }
        else
        {
            _currentRotation = 0;
        }

        _targetRotation = Mathf.SmoothDampAngle(_targetRotation, _currentRotation, ref _velocity, _rotationSmoothTime);
        _target.rotation = Quaternion.Euler(0, _target.eulerAngles.y, -_targetRotation);
    }
}
