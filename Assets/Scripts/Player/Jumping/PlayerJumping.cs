using UnityEngine;

public class PlayerJumping
{
    private PlayerGameStatus _status;
    private float _jumpHeight;

    public PlayerJumping(PlayerJumpingData data, PlayerGameStatus status)
    {
        _jumpHeight = data.JumpHeight;
        _status = status;
    }


}
