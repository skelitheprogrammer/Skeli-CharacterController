using UnityEngine;
using Zenject;

public class FreeFormMovementModule : IMovementModule
{
    [Inject] private readonly DirectionController _directionController;
    [Inject] private readonly PlayerMovementData _moveData;

    private Vector3 _goalVel;
    private Vector3 _neededAccel;
    private float _accel;

    public Vector3 CalculateMovement(in Vector3 velocity)
    {
        var maxSpeed = _moveData.MaxSpeed;
        var acceleration = _moveData.Acceleration;
        var accelCurve = _moveData.AccelerationCurve;
        var direction = _directionController.GetCameraSlopeVector();

        var goalVel = direction * maxSpeed;
        var velDot = _directionController.GetDot();

        _accel = acceleration * accelCurve.Evaluate(velDot);

        _goalVel = Vector3.MoveTowards(_goalVel, goalVel, _accel * Time.deltaTime);

        _neededAccel = _goalVel - velocity + Vector3.up * velocity.y;

        return _neededAccel;
    }
}
