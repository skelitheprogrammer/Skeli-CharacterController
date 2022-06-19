using UnityEngine;

public class FreeFormRotationModule : IRotationModule
{
    private Vector3 _velocity;

    public Quaternion CalculateRotationAngle(object incomeData)
    {
        var data = incomeData as FreeFormRotationData;
        var input = data.input;

        float targetRotation = 0;

        if (data.input != Vector2.zero)
        {
            targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + data.rotation.y;
        }

        var smoothTargetRotation = Vector3.SmoothDamp(data.currentRotation, new Vector3(0, targetRotation,0), ref _velocity, data.smoothTime);    
        return Quaternion.Euler(smoothTargetRotation);
    }
}

public class FreeFormRotationData
{
    public Vector2 input;
    public Vector3 currentRotation;
    public Vector3 rotation;
    public float smoothTime;
}
