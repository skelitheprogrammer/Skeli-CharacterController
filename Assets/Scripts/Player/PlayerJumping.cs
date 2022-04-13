using UnityEngine;

public class PlayerJumping
{
    private DirectionController _direction;
    private float _jumpHeight;

    public PlayerJumping(DirectionController direction, PlayerJumpingData data)
    {
        _direction = direction;
        _jumpHeight = data.JumpHeight;
    }

    public void Jump(ref Vector3 velocity)
    {
        var jumpHeight = Mathf.Sqrt(-2 * _jumpHeight * Physics.gravity.y);
        var jumpForce = jumpHeight * _direction.JumpVector;
        velocity += jumpForce;
    }
}
