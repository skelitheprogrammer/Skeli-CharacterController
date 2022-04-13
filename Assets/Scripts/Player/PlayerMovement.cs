using UnityEngine;
using Zenject;

public class PlayerMovement
{
    private float _acceleration = 200;
    private float _maxAcceleration = 150;
    private float _maxSpeed = 10;

    private AnimationCurve _accelerationCurve;
    private AnimationCurve _maxAccelerationCurve;
    
    private DirectionController _direction;
    private InputReader _input;

    private Vector3 _moveDirection;
    private Vector3 _goalVel;
    private Vector3 _neededAccel;

    public PlayerMovement(PlayerMovementData data, InputReader input, DirectionController direction)
    {
        _acceleration = data.Acceleration;
        _maxAcceleration = data.MaxAcceleration;
        _maxSpeed = data.MaxSpeed;
        _accelerationCurve = data.AccelerationCurve;
        _maxAccelerationCurve = data.MaxAccelerationCurve;

        _input = input;
        _direction = direction;
    }

    public void CharacterMove(ref Vector3 velocity)
    {
        _moveDirection = _direction.LookSlopeVector * _input.MoveInput.magnitude;
        var unitGoal = _moveDirection;

        var unitVel = _goalVel.normalized;

        var velDot = Vector3.Dot(unitGoal, unitVel);
        
        var maxAccel = _maxAcceleration * _maxAccelerationCurve.Evaluate(velDot);

        _neededAccel = _goalVel / Time.deltaTime;
        _neededAccel = Vector3.ClampMagnitude(_neededAccel, maxAccel);

        var accel = (_acceleration + _neededAccel.magnitude) * _accelerationCurve.Evaluate(velDot);
        
        var goalVel = unitGoal * _maxSpeed;

        _goalVel = Vector3.MoveTowards(_goalVel, goalVel, accel * Time.deltaTime);
        velocity = _goalVel;
    }

}
