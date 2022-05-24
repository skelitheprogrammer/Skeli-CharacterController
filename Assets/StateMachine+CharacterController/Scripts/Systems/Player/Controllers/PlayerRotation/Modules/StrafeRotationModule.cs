using UnityEngine;
using Zenject;

public class StrafeRotationModule : IRotationModule
{
    [Inject] private readonly PlayerRotationData _data;

    private float _velocity;

    public Quaternion CalculateRotationAngle(Vector2 direction, Vector3 currentRotation, Vector3 targetRotation, ref float targetShit)
    {
        targetShit = Mathf.SmoothDampAngle(currentRotation.y, targetRotation.y, ref _velocity, _data.RotationSmoothTime);
        return Quaternion.Euler(0, targetShit, 0);
    }
}