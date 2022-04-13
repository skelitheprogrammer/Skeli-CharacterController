using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private PlayerDirectionController _direction;
    [SerializeField] private PlayerGroundDetection _groundDetection;
    [SerializeField] private PlayerGravity _gravity;
    [SerializeField] private float _jumpForce;

    public void TryJump()
    {
        if (_input.IsJumped)
        {
            Jump();
        }
    }

    private void Jump()
    {
        var jumpHeight = Mathf.Sqrt(-2 * _jumpForce * Physics.gravity.y);
        var jumpForce = jumpHeight * _direction.JumpVector;
        _gravity.AddForce(jumpForce);
    }
}
