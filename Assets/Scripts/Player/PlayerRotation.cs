using JetBrains.Annotations;
using UnityEngine;
using Zenject;

public class PlayerRotation
{
    [Inject(Id = Constants.MainCamera)] private Camera _camera;

    private float _rotationSmoothTime;

    private float _targetRotation;
    private float _rotationVelocity;

    public PlayerRotation(PlayerRotationData data)
    {
        _rotationSmoothTime = data.RotationSmoothTime;
    }

    public void CharacterRotate(Transform transform, Vector2 input)
    {
        if (input == Vector2.zero) return;

        _targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationSmoothTime);

        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }
}

public static class Constants
{
    public const string MainCamera = "MainCamera";
}