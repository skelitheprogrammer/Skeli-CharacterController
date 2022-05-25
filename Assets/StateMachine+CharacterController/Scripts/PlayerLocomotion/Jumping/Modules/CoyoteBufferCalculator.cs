using UnityEngine;
using Zenject;

public class CoyoteBufferCalculator
{
    [Inject] private readonly PlayerJumpData _jumpData;
    [Inject] private readonly GroundCheckController _groundCheck;

    private float _coyoteTimer;
    private float _bufferTimer;

    private void UpdateCoyoteTime(out bool canJump)
    {
        if (_coyoteTimer < _jumpData.JumpCoyoteTime)
        {
            canJump = true;
            _coyoteTimer += Time.deltaTime;
            return;
        }

        canJump = false;
    }

    private void UpdateBufferTime(out bool canJump)
    {
        if (_bufferTimer < _jumpData.JumpBufferTime)
        {
            canJump = false;
            _bufferTimer += Time.deltaTime;
            return;
        }

        canJump = true;
    }


    public void UpdateTimer(out bool canJump)
    {
        if (_groundCheck.GroundCheck())
        {
            _coyoteTimer = 0;
            UpdateBufferTime(out canJump);
        }
        else
        {
            _bufferTimer = 0;
            UpdateCoyoteTime(out canJump);
        }
    }
}
