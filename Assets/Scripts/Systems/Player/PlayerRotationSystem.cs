using UnityEngine;
using Zenject;

public class PlayerRotationSystem : GameSystem
{
    [Inject(Id = Constants.PLAYERTRANSFORM)] private Transform _playerTransform;
    [Inject(Id = Constants.ROTATEORIGIN)] private Transform _rotateOrigin;
    [Inject] private InputReader _input;

    [Inject] private PlayerRotationData _data;

    private float _targetRotation;
    private float _rotationVelocity;

    public override void DoLogic()
    {
        CharacterRotate();
    }

    private void CharacterRotate()
    {
        if (_input.MoveInput != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(_input.MoveInput.x, _input.MoveInput.y) * Mathf.Rad2Deg + _rotateOrigin.eulerAngles.y;
        }
        var rotation = Mathf.SmoothDampAngle(_playerTransform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _data.RotationSmoothTime);

        _playerTransform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }

}
