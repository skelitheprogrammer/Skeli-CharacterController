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
        var maxAcceleration = _moveData.MaxAcceleration;
        var maxAccelCurve = _moveData.MaxAccelerationCurve;
        
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
        var maxAccel = maxAcceleration * maxAccelCurve.Evaluate(velDot);

        /*        _goalVel = Vector3.MoveTowards(_goalVel, goalVel, accel * Time.deltaTime);

                var difference = (_goalVel - velocity) + Vector3.up * velocity.y;
                difference = Vector3.ClampMagnitude(difference, maxSpeed);
                Debug.Log($"{velocity.magnitude:0.00} | {difference.magnitude:0.00} | {_goalVel.magnitude:0.00} | {velDot:0.00}");
                return difference;*/

        _goalVel = Vector3.MoveTowards(_goalVel, goalVel, accel * Time.deltaTime);

        var difference = _goalVel - velocity;
        //difference = Vector3.ClampMagnitude(difference, maxSpeed);
        Debug.Log($"{velocity.magnitude:0.00} | {difference.magnitude:0.00} | {_goalVel.magnitude:0.00} | {velDot:0.00}");
        return difference + Vector3.up * velocity.y;
    }
}
