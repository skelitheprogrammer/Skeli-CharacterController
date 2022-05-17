using UnityEngine;
using Zenject;

public class PlayerJumpController : PlayerJumpControllerBase
{
    [Inject] private readonly CoyoteBufferCalculator _coyoteBuffer;
    [Inject] private readonly PlayerJumpCalculator _jumpForce;

    private bool _canJump;

    public override bool CanJump => _canJump;

    public override Vector3 CalculateJumpForce()
    {
        if (_canJump)
        {
            return _jumpForce.CalculateJumpForce();
        }

        return Vector3.zero;
    }

    public override void CalculateCanJump()
    {
        _coyoteBuffer.UpdateTimer(out _canJump);
    }
}
