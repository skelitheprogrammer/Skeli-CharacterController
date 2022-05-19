using UnityEngine;
using Zenject;

public class FreeFormMovementModule : IMovementModule
{
    [Inject] private readonly GroundCheckController _groundCheck;
    [Inject] private readonly DirectionController _directionController;
    [Inject] private readonly PlayerMovementData _moveData;

    private Vector3 _goalVel;
    private Vector3 _neededAccel;
    private Vector3 _previousDirection;
    private float _accel;

    public Vector3 CalculateMovement(Vector3 velocity)
    {
        var maxSpeed = _moveData.MaxSpeed;
        var acceleration = _moveData.Acceleration;
        var accelCurve = _moveData.AccelerationCurve;
        var direction = _directionController.GetCameraSlopeVector();

        if (!_groundCheck.GroundCheck())
        {
            direction = (_previousDirection + direction).normalized;
        }

        _previousDirection = direction;

        var goalVel = direction * maxSpeed;
        var velDot = _directionController.GetDot();

        _accel = acceleration * accelCurve.Evaluate(velDot);

        _goalVel = Vector3.MoveTowards(_goalVel, goalVel, _accel * Time.deltaTime);

        _neededAccel = _goalVel - velocity + Vector3.up * velocity.y;

        if (!_groundCheck.GroundCheck())
        {
            _neededAccel.y = 0;
        }

        return _neededAccel;
    }
}

public class StrafeMovementModule : IMovementModule
{
    public Vector3 CalculateMovement(Vector3 velocity)
    {
        return Vector3.zero;
    }
}