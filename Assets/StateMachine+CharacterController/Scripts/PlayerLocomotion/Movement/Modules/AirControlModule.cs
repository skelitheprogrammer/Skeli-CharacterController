using UnityEngine;
using Zenject;

public class AirControlModule : IMovementModule
{
    [Inject] private readonly DirectionController _directionController;
    [Inject] private readonly PlayerMovementData _moveData;

    private Vector3 _previousDirection;

    public Vector3 CalculateMovement(in Vector3 velocity)
    {
        var maxSpeed = _moveData.AirMaxSpeed;
        var acceleration = _moveData.Acceleration;
        var accelCurve = _moveData.AccelerationCurve;
        var direction = _directionController.GetCameraSlopeVector();


        return velocity;

    }
}