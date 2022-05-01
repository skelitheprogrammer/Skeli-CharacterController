using UnityEngine;
using Zenject;

public class PlayerJumpSystem
{
    [Inject] private CharacterStateData _data;
    [Inject] private PlayerJumpData _jumpData;

    public Vector3 CalculateJumpForce()
    {
        var direction = _data.jumpVector;
        var jumpHeight = Mathf.Sqrt(-2 * Physics.gravity.y * _jumpData.JumpHeight);
        return direction * jumpHeight;
    }
}