using UnityEngine;

public class PlayerJumping
{
    private DirectionController _direction;
    private float _jumpForce;

    public PlayerJumping(DirectionController direction, float jumpForce)
    {
        _direction = direction;
        _jumpForce = jumpForce;
    }

    public void Jump(ref Vector3 velocity)
    {
        var jumpHeight = Mathf.Sqrt(-2 * _jumpForce * Physics.gravity.y);
        var jumpForce = jumpHeight * _direction.JumpVector;
        velocity += jumpForce;
    }
}
