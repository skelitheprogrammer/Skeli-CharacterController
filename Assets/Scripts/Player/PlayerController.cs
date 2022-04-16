using System;
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
    [Inject] private GroundDetection _ground;
    [Inject] private Rigidbody _rb;

    private void Update()
    {
        var moveDirection = _input.MoveInput.magnitude * _direction.LookSlopeVector;
        var force = _movement.CalculateMoveVector(moveDirection);
        _rb.AddForceAtPosition(force * _rb.mass, transform.position + Vector3.up * (transform.localScale.y * +.25f));
        
        _rotation.CharacterRotate(transform, _input.MoveInput);

    }


}
