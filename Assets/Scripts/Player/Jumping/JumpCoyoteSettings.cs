using UnityEngine;
using Zenject;

public class JumpCoyoteSettings
{
    private float _coyoteTime;
    private float _jumpBuffer;

    private float _coyoteTimer;
    private float _bufferTimer;

    [Inject] private GroundDetection _detection;
    [Inject] private PlayerGameStatus _status;

    public JumpCoyoteSettings(PlayerJumpingData data)
    {
        _coyoteTime = data.CoyoteTime;
        _jumpBuffer = data.JumpBuffer;
    }

    public void Update()
    {
        if (_detection.IsDetected && !_status.canJump)
        {
            _coyoteTimer = 0;
            CalculateTime(ref _bufferTimer, _jumpBuffer, true);
        }
        else if (!_detection.IsDetected && _status.canJump)
        {
            _bufferTimer = 0;
            CalculateTime(ref _coyoteTimer, _coyoteTime, false);
        }
    }

    private void CalculateTime(ref float timer, float initTime, bool endCondition)
    {
        timer += Time.deltaTime;

        if (timer >= initTime)
        {
            timer = initTime;
            _status.canJump = endCondition;
        }
    }
}