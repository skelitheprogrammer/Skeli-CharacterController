using UnityEngine;
using Zenject;

public class PlayerJumpCalculator 
{ 
    [Inject] private readonly DirectionController _directionController;
    [Inject] private readonly PlayerJumpData _jumpData;

    public Vector3 CalculateJumpForce()
    {
        var direction = _directionController.GetJumpVector();
        var jumpForce = _jumpData.JumpHeight;
/*
        if (_directionController.IsOnSlope)
        {
            jumpForce *= 2;
        }
*/
        var jumpHeight = Mathf.Sqrt(-2 * Physics.gravity.y * jumpForce);

        return direction * jumpHeight;
    }
}