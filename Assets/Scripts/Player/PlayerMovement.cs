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


    }

}
