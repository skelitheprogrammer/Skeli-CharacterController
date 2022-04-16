using UnityEngine;
using Zenject;

public class PlayerMovement
{
    private float _acceleration = 200;
    private float _maxAccelerationForce = 150;
    private float _maxSpeed = 10;

    private AnimationCurve _accelerationCurve;
    private AnimationCurve _maxAccelerationCurve;

    [Inject] private PlayerGameStatus _status;

    private Vector3 _goalVel;
    private Vector3 _neededAccel;

    public PlayerMovement(PlayerMovementData data)
    {
        _acceleration = data.Acceleration;
        _maxAccelerationForce = data.MaxAcceleration;
        _maxSpeed = data.MaxSpeed;
        _accelerationCurve = data.AccelerationCurve;
        _maxAccelerationCurve = data.MaxAccelerationCurve;
    }

    public Vector3 CalculateMoveVector(Vector3 direction)
    {
        var unitVel = _goalVel.normalized;

        var velDot = Vector3.Dot(direction, unitVel);

        var accel = _acceleration * _accelerationCurve.Evaluate(velDot);
        var goalVel = direction * _maxSpeed;

        _goalVel = Vector3.MoveTowards(_goalVel, goalVel, accel * Time.deltaTime);

        var maxAccel = _maxAccelerationForce * _maxAccelerationCurve.Evaluate(velDot);

        _neededAccel = (_goalVel - _status.velocity) / Time.deltaTime;
        _neededAccel = Vector3.ClampMagnitude(_neededAccel, maxAccel);

        Debug.Log($"{_goalVel.magnitude} | {_neededAccel.magnitude} | {_status.velocity.magnitude}");
        return _neededAccel;



    }

}
