using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{

    [Inject] private PlayerMovement _movement;
    [Inject] private PlayerRotation _rotation;
    [Inject] private PlayerGravity _gravity;
    [Inject] private PlayerJumpController _jumpController;

    [Inject] private PlayerGameStatus _status;
    [Inject] private InputReader _input;
    [Inject] private DirectionController _direction;

    public Vector3 test;

    private void Update()
    {


        
        _rotation.CharacterRotate(transform, _input.MoveInput);
       
        //_gravity.ApplyGravity();




    }

    private void FixedUpdate()
    {
        var moveDirection = _direction.LookSlopeVector * _input.MoveInput.magnitude;
        _movement.CharacterMove(moveDirection);

        _jumpController.Procceed();
            _jumpController.Jump(_direction.JumpVector);

        if (_input.IsJumped && _status.canJump)
        {
        }
    }
}
