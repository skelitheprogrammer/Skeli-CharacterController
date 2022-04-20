using UnityEngine;
using Zenject;

public class PlayerSimpleMovementSystem
{
    [Inject] private InputReader _input;
    [Inject] private PlayerMovementData _moveData;
    [Inject] private CharacterStateData _data;
    private Vector3 _goalVel;
    private Vector3 _lastDirection;

    public Vector3 CalculateMovement(Vector3 velocity)
    {
        var directionVel =  _input.MoveInputDirection.normalized;

        var maxSpeed = _moveData.MaxSpeed;
        var acceleration = _moveData.Acceleration;
        var accelCurve = _moveData.AccelerationCurve;
        
        if (_data.isGrounded)
        {
            _lastDirection = directionVel;
        }
        else
        {
            directionVel = _lastDirection + (directionVel * maxSpeed );
        }

        var goalVel = directionVel * maxSpeed;

        var velDot = Vector3.Dot(_input.MoveInputDirection, _goalVel.normalized);
        var accel = acceleration * accelCurve.Evaluate(velDot);

        _goalVel = Vector3.MoveTowards(_goalVel, goalVel, accel * Time.deltaTime);

        var difference = _goalVel - velocity;
        return difference + Vector3.up * velocity.y;
    }
}
