using UnityEngine;
using Zenject;

public class PlayerJumpController
{
    [Inject] private readonly CoyoteBufferCalculator _coyoteBuffer;
    [Inject] private readonly PlayerJumpCalculator _jumpForce;

    private bool _canJump;

    public bool CanJump => _canJump;

    public  Vector3 CalculateJumpForce()
    { 
        if (_canJump)
        {
            return _jumpForce.CalculateJumpForce();
        }

        return Vector3.zero;
    }

    public void CalculateCanJump()
    {
        _coyoteBuffer.UpdateTimer(out _canJump);
    }
}
