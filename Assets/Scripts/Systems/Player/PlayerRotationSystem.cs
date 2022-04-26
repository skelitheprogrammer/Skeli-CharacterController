using UnityEngine;
using Zenject;

public class PlayerRotationSystem : IInitializable
{
    [Inject] private readonly CharacterStateData _data;
    [Inject] private readonly InputReader _input;
    [Inject] private readonly PlayerRotationData _rotationData;

    private float _targetRotation;
    private float _rotationVelocity;

    public void Initialize()
    {
        _targetRotation = _data.playerTransform.eulerAngles.y;
    }

    public Quaternion CalculateRotationAngle()
    {
        if (_input.MoveInput != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(_input.MoveInput.x, _input.MoveInput.y) * Mathf.Rad2Deg + _data.rotateOrigin.eulerAngles.y;
        }

        var rotation = Mathf.SmoothDampAngle(_data.playerTransform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationData.RotationSmoothTime);    
        return Quaternion.Euler(0, rotation, 0);
    }

}
