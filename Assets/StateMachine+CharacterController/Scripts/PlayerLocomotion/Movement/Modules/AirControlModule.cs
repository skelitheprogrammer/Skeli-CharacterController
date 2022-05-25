using UnityEngine;
using Zenject;

public class AirControlModule : IMovementModule
{
    [Inject] private readonly DirectionController _directionController;
    [Inject] private readonly PlayerMovementData _moveData;

    public Vector3 CalculateMovement(in Vector3 velocity)
    {
        var maxSpeed = _moveData.MaxSpeed;
        var airSpeed = _moveData.AirStrafeSpeed;
        var direction = _directionController.GetCameraSlopeVector();
        var velDot = _directionController.GetDot();

        var goalVel = Vector3.zero;
        var check = new Vector2(velocity.x, velocity.z).magnitude;

        Debug.Log(check);
        if (velDot <= .9f || check <= maxSpeed)
        {
            goalVel = airSpeed * Time.deltaTime * direction; 
        }

        return goalVel;
    }
}