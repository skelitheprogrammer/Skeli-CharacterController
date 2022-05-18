using UnityEngine;
using Zenject;

public class FreeFormRotationModule : IRotationModule
{
    [Inject(Id = IDConstants.PLAYERTRANSFORM)] private readonly Transform _player;
    [Inject(Id = IDConstants.ROTATEORIGIN)] private readonly Transform _origin;

    [Inject] private readonly InputReader _input;
    [Inject] private readonly PlayerRotationData _rotationData;

    private float _targetRotation;
    private float _rotationVelocity;

    public Quaternion CalculateRotationAngle()
    {
        if (_input.MoveInput != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(_input.MoveInput.x, _input.MoveInput.y) * Mathf.Rad2Deg + _origin.eulerAngles.y;
        }

        var rotation = Mathf.SmoothDampAngle(_player.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationData.RotationSmoothTime);    
        return Quaternion.Euler(0, rotation, 0);
    }
}

public class StrafeRotationModule : IRotationModule
{
    [Inject(Id = IDConstants.ROTATEORIGIN)] private readonly Transform _origin;

    public Quaternion CalculateRotationAngle()
    {
        return Quaternion.Euler(0, _origin.eulerAngles.y, 0);
    }
}