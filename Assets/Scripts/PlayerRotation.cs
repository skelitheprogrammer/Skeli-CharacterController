using UnityEngine;

public class PlayerRotation : MonoBehaviour 
{
    [SerializeField] private InputReader _input;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _rotationSmoothTime;

    private float _targetRotation;
    private float _rotationVelocity;

    public void CharacterRotate()
    {
        if (_input.MoveInput == Vector2.zero) return;

        _targetRotation = Mathf.Atan2(_input.MoveInput.x, _input.MoveInput.y) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationSmoothTime);

        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }
}