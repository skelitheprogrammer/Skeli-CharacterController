using UnityEngine;
using Zenject;

public class StrafeRotationModule : IRotationModule
{
    [Inject(Id = IDConstants.ROTATEORIGIN)] private readonly Transform _origin;

    public Quaternion CalculateRotationAngle()
    {
        return Quaternion.Euler(0, _origin.eulerAngles.y, 0);
    }
}