using UnityEngine;

public sealed class RotationService : ControllerBase<IRotationModule>
{
    public Quaternion CalculatePlayerRotation(object data)
    {
        return _module.CalculateRotationAngle(data);
    }
}
