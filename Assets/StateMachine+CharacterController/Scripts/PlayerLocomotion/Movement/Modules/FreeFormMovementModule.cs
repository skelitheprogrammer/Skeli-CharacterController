using UnityEngine;
using Zenject;

public class FreeFormMovementModule : IMovementModule
{
    [Inject] private readonly DirectionController _directionController;
    [Inject] private readonly PlayerMovementData _moveData;

    public Vector3 CalculateMovement(in Vector3 velocity)
    {
        var goalVel = _directionController.GetCameraSlopeVector() * _moveData.MaxSpeed;
        var velDot = _directionController.GetDot();
        var accel = _moveData.Acceleration * _moveData.AccelerationCurve.Evaluate(velDot);

        var _goalVel = velocity;

        _goalVel = Vector3.MoveTowards(_goalVel, goalVel, accel * Time.deltaTime);

        var neededAccel = _goalVel - velocity;

        return neededAccel;
    }
}
