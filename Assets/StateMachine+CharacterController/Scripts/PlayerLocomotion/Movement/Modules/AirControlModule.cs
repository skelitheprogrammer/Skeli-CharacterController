using UnityEngine;
using Zenject;

public class AirControlModule : IMovementModule
{
    [Inject] private readonly DirectionController _directionController;
    [Inject] private readonly PlayerMovementData _moveData;

    public Vector3 CalculateMovement(in Vector3 velocity)
    {
        var velDot = _directionController.GetDot();
        var accel = _moveData.Acceleration * _moveData.AccelerationCurve.Evaluate(velDot);

        var goalVel = _directionController.GetCameraVector() * _moveData.AirStrafeSpeed;
        var velocityDirection = new Vector3(velocity.x,0, velocity.z);
        var neededAccel = Vector3.zero;

        if (goalVel != Vector3.zero && velDot <= .97f)
        {
            Vector3 currentVelocity = Vector3.MoveTowards(velocityDirection, goalVel, accel * Time.deltaTime);

            neededAccel = currentVelocity - velocity + Vector3.up * velocity.y;
        }

        return neededAccel;
    }
}