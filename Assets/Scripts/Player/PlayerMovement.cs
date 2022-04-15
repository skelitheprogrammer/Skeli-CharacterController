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
    [Inject] private Rigidbody _rb;

    private Vector3 _goalVel;

    public PlayerMovement(PlayerMovementData data)
    {
        _acceleration = data.Acceleration;
        _maxAccelerationForce = data.MaxAcceleration;
        _maxSpeed = data.MaxSpeed;
        _accelerationCurve = data.AccelerationCurve;
        _maxAccelerationCurve = data.MaxAccelerationCurve;
    }

    public void CharacterMove(Vector3 direction)
    {

        float velDot = Vector3.Dot(direction, _goalVel.normalized);
        float accel = _acceleration * _accelerationCurve.Evaluate(velDot);
       
        Vector3 goalVel = direction * _maxSpeed;

        _goalVel = Vector3.MoveTowards(_goalVel, goalVel, accel * Time.fixedDeltaTime);
        Vector3 neededAccel = (_goalVel - _rb.velocity) / Time.fixedDeltaTime;
        float maxAccel = _maxAccelerationForce * _maxAccelerationCurve.Evaluate(velDot);
        neededAccel = Vector3.ClampMagnitude(neededAccel, maxAccel);
        _rb.AddForce(neededAccel * _rb.mass);

    }

}
