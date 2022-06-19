using UnityEngine;

public class AirControlModule : IMovementModule
{
    private readonly AirControlModuleData _data;

    public AirControlModule(AirControlModuleData data)
    {
        _data = data;
    }

    public Vector3 CalculateMovement()
    {

        var accel = _data.acceleration * _data.accelFactor;

        var goalVel = _data.direction * _data.speed;
        var velocityDirection = new Vector3(_data.velocity.x, 0, _data.velocity.z);

        if (goalVel != Vector3.zero)
        {
            Vector3 currentVelocity = Vector3.MoveTowards(velocityDirection, goalVel, accel * Time.deltaTime);

            if (currentVelocity.magnitude <= goalVel.magnitude)
            {
                return currentVelocity - _data.velocity + Vector3.up * _data.velocity.y;
            }
        }

        return Vector3.zero;
    }
}

public class AirControlModuleData
{
    public Vector3 velocity;
    public Vector3 direction;
    public float speed;
    public float acceleration;
    public float accelFactor;
}