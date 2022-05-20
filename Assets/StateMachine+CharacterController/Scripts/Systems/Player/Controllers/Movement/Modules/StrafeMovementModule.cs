using UnityEngine;
using Zenject;

public class StrafeMovementModule : IMovementModule
{
    [Inject] private readonly PlayerMovementData _data;
    [Inject] private readonly InputReader _input;
    [Inject] private readonly DirectionController _directionController;

    public Vector3 CalculateMovement(Vector3 velocity)
    {
        var direction = _directionController.GetCameraSlopeVector();
        var inputDirection = _input.MoveInputDirection;

        var forwardAmount = Vector3.Dot(_directionController.GetLookSlopeVector(), _directionController.GetPlayerForwardInput());

        var speed = 0f;

        switch (forwardAmount)
        {
            case < -.5f:
                speed = _data.BackwardsSpeed;
                break;
            case < .5f:
                speed = _data.SideWaysSpeed;
                break;
            default:
                speed = _data.ForwardSpeed;
                break;

        }

        var targetVelocity = direction * speed;

        var neededAccel = targetVelocity - velocity + Vector3.up * velocity.y;

        return neededAccel;
    }

}