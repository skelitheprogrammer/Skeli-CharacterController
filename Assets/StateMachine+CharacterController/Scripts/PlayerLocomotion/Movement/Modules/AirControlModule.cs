using UnityEngine;
using Zenject;

public class AirControlModule : IMovementModule
{
    [Inject] private readonly DirectionController _directionController;
    [Inject] private readonly PlayerMovementData _moveData;

    private Vector3 _neededAccel;
    private Vector3 _goalVel;

    public Vector3 CalculateMovement(in Vector3 velocity)
    {
        _neededAccel = Vector3.zero;

        var airSpeed = _moveData.AirStrafeSpeed; 
        var acceleration = _moveData.Acceleration;
        var accelCurve = _moveData.AccelerationCurve;
        var velDot = _directionController.GetDot();

        var velocityDirection = new Vector3(velocity.x,0, velocity.z);
        var newDirection = _directionController.GetCameraVector();
        var accel = acceleration * accelCurve.Evaluate(velDot);

        var goalVel = newDirection * airSpeed;

        if (goalVel != Vector3.zero && velDot <= .97f)
        {
            _goalVel = Vector3.MoveTowards(velocityDirection, goalVel, accel * Time.deltaTime);

            _neededAccel = _goalVel - velocity + Vector3.up * velocity.y;
        }

        return _neededAccel;
    }
}