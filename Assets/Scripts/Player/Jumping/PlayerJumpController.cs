using UnityEngine;
using Zenject;

public class PlayerJumpController
{
    [Inject] private JumpCoyoteSettings _coyote;
    [Inject] private PlayerGravity _gravity;
    [Inject] private Rigidbody _rb;
    
    private float _jumpHeight;

    public PlayerJumpController(PlayerJumpingData data)
    {
        _jumpHeight = data.JumpHeight;
    }

    public void Procceed()
    {
        _coyote.Update();
    }

    public void Jump(Vector3 direction)
    {
        var jumpHeight = Mathf.Sqrt(-2 * _jumpHeight * Physics.gravity.y);
        var jumpForce = jumpHeight * direction;

        //_gravity.AddForce(jumpForce);
        _rb.AddForce(jumpForce);
    }
}
