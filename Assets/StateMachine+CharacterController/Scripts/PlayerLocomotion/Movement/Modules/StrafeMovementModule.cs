using UnityEngine;
using Zenject;

public class StrafeMovementModule : IMovementModule
{
    [Inject] private readonly PlayerMovementData _data;
    [Inject] private readonly DirectionController _directionController;

    public Vector3 CalculateMovement(in Vector3 velocity)
    {
        var direction = _directionController.GetCameraSlopeVector();

        var forwardAmount = Vector3.Dot(_directionController.GetLookSlopeVector(), _directionController.GetPlayerForwardInput());

        var speed = forwardAmount switch
        {
            < -.5f => _data.BackwardsSpeed,
            < .5f => _data.SideWaysSpeed,
            _ => _data.ForwardSpeed,
        };

        var targetVelocity = direction * speed;

        return targetVelocity;
    }

}