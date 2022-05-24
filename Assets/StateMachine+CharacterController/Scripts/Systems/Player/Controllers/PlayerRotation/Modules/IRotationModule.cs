using UnityEngine;

public interface IRotationModule
{
    Quaternion CalculateRotationAngle(Vector2 direction, Vector3 currentRotation, Vector3 targetRotation, ref float targetShit);
}
