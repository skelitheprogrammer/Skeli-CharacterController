using UnityEngine;
using Zenject;

public class PlayerJumpCalculator : GameSystem
{
    [Inject] private readonly DirectionController _directionController;
    [Inject] private readonly PlayerJumpData _jumpData;

    public Vector3 CalculateJumpForce()
    {
        if (!_enabled) return Vector3.zero;

        var direction = _directionController.GetJumpVector();
        var jumpHeight = Mathf.Sqrt(-2 * Physics.gravity.y * _jumpData.JumpHeight);

        return direction * jumpHeight;
    }
}