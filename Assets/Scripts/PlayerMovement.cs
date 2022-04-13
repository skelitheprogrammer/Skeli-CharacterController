using UnityEngine;

public class PlayerMovement
{
    [SerializeField] private float _acceleration = 200;
    [SerializeField] private float _maxAcceleration = 150;
    [SerializeField] private float _maxSpeed = 10;

    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerDirectionController _direction;

    [SerializeField] private AnimationCurve _accelerationCurve;
    [SerializeField] private AnimationCurve _maxAccelerationCurve;
    
    [SerializeField] private CharacterController _controller;
    [SerializeField] private InputReader _input;

    private Vector3 _moveDirection;
    private Vector3 _goalVel;
    private Vector3 _neededAccel;

    public void CharacterMove()
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
        
        _controller.Move(_goalVel * Time.deltaTime);
    }

}
