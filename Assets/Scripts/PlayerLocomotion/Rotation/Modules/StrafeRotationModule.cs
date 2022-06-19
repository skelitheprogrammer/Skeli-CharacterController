using UnityEngine;

public class StrafeRotationModule : IRotationModule
{
    public Quaternion CalculateRotationAngle(object incomeData)
    {
        var data = incomeData as StrafeRotationData;

        return Quaternion.Euler(0, data.desiredRotation, 0);
    }
}

public class StrafeRotationData
{
    public float desiredRotation;
}