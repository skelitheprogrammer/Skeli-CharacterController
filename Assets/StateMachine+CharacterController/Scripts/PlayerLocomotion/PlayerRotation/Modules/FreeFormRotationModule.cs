using UnityEngine;
using Zenject;

public class FreeFormRotationModule : IRotationModule
{
    [Inject] private readonly PlayerRotationData _rotationData;

    private float _velocity;

    public Quaternion CalculateRotationAngle(Vector2 direction, Vector3 currentRotation, Vector3 targetRotation, ref float targetShit)
    {
        if (direction != Vector2.zero)
        {
            targetShit = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + targetRotation.y;
        }

        var rotation = Mathf.SmoothDampAngle(currentRotation.y, targetShit, ref _velocity, _rotationData.RotationSmoothTime);    
        return Quaternion.Euler(0, rotation, 0);
    }
}
