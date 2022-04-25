using UnityEngine;
using Zenject;

public class PlayerRotationSystem
{
    [Inject] private readonly CharacterStateData _data;
    [Inject] private readonly InputReader _input;
    [Inject] private readonly PlayerRotationData _rotationData;

    private float _targetRotation;
    private float _rotationVelocity;

    public void CharacterRotate()
    {
        if (_input.MoveInput != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(_input.MoveInput.x, _input.MoveInput.y) * Mathf.Rad2Deg + _data.rotateOrigin.eulerAngles.y;
        }

        var rotation = Mathf.SmoothDampAngle(_data.playerTransform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationData.RotationSmoothTime);

        _data.playerTransform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }

}
