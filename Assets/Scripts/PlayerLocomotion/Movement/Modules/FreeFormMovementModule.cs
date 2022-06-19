using UnityEngine;

public class FreeFormMovementModule : IMovementModule
{
    private FreeFormMovementData _data;

    public FreeFormMovementModule(FreeFormMovementData data)
    {
        _data = data;
    }

    public Vector3 CalculateMovement()
    {
        var goalVel = _data.direction * _data.speed;

        var accel = _data.acceleration * _data.accelFactor;

        var _goalVel = _data.velocity;

        _goalVel = Vector3.MoveTowards(_goalVel, goalVel, accel * Time.deltaTime);

        var neededAccel = _goalVel - _data.velocity;

        return neededAccel;
    }
}

public class FreeFormMovementData
{
    public Vector3 velocity;
    public Vector3 direction;
    public float speed;
    public float acceleration;
    public float accelFactor;
}