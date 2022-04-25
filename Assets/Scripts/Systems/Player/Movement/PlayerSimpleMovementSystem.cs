using UnityEngine;
using Zenject;

public class PlayerSimpleMovementSystem
{
    [Inject] private PlayerMovementData _moveData;
    [Inject] private CharacterStateData _data;

    private Vector3 _goalVel;

    public Vector3 CalculateMovement()
    {
        var maxSpeed = _moveData.MaxSpeed;
        var acceleration = _moveData.Acceleration;
        var accelCurve = _moveData.AccelerationCurve;
        var direction = _data.playerDirection;
        var goalVel = direction * maxSpeed;

        ref var neededAccel = ref _data.neededAccel;

        var velDot = Vector3.Dot(direction, _goalVel.normalized);
        var accel = acceleration * accelCurve.Evaluate(velDot);

        _goalVel = Vector3.MoveTowards(_goalVel, goalVel, accel * Time.deltaTime);

        neededAccel = _goalVel - _data.velocity + Vector3.up * _data.velocity.y;

        return neededAccel;
    }
}
