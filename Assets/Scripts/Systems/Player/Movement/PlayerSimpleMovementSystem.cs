using UnityEngine;
using Zenject;

public class PlayerSimpleMovementSystem
{
    [Inject] private InputReader _input;
    [Inject] private PlayerMovementData _moveData;
    [Inject] private CharacterStateData _data;

    private Vector3 _goalVel;
    private Vector3 _lastDirection;

    public Vector3 CalculateMovement()
    {
        var maxSpeed = _moveData.MaxSpeed;
        var acceleration = _moveData.Acceleration;
        var accelCurve = _moveData.AccelerationCurve;
        var direction = _data.cameraSlopeVector * _input.MoveInput.magnitude;

        if (!_data.isGrounded)
        {
            direction = (_lastDirection + direction).normalized;
        }

        _lastDirection = direction;

        var velDot = Vector3.Dot(direction, _goalVel.normalized);
        var accel = acceleration * accelCurve.Evaluate(velDot);

        var goalVel = direction * maxSpeed;
        _goalVel = Vector3.MoveTowards(_goalVel, goalVel, accel * Time.deltaTime);

        var difference = _goalVel - _data.velocity + Vector3.up * _data.velocity.y;

        if (!_data.isGrounded)
        {
            difference.y = 0;
        }

        return difference;
    }
}
