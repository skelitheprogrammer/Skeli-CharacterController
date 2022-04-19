using FSM;
using System;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Inject] private GroundCheckController _groundCheck;
    [Inject] private DirectionController _directionController;

    [Inject] private GravitySystem _gravity;
    [Inject] private PlayerJumpSystem _jump;
    [Inject] private PlayerSimpleMovementSystem _movement;

    [Inject] private InputReader _input;

    [Inject] private CharacterStateData _data;
    [Inject] private StateMachine _fsm;
    [Inject] private CharacterController _controller;

    public Vector3 velocity;

    private void Awake()
    {
        _fsm.AddState("IsGrounded", new State());
        _fsm.AddState("IsJumping", new State((enter) => ApplyValue(_jump.CalculateJumpForce())));
        _fsm.AddState("IsFalling", new State());

        _fsm.AddTransition(new Transition("IsGrounded", "IsJumping", (condition) => _input.IsJumped && _data.isGrounded));
        _fsm.AddTransition(new Transition("IsGrounded", "IsFalling", (condition) => velocity.y < 0 && !_data.isGrounded));
        _fsm.AddTransition(new Transition("IsJumping", "IsGrounded", (condition) => _data.isGrounded));

        _fsm.AddTransition(new Transition("IsJumping", "IsFalling", (condition) => velocity.y < 0 && !_data.isGrounded));
        _fsm.AddTransition(new Transition("IsFalling", "IsGrounded", (condition) => _data.isGrounded));

        _fsm.SetStartState("IsGrounded");
    }

    private void Start()
    {
        _fsm.Init();
    }

    private void Update()
    {
        _groundCheck.Procceed();
        _directionController.Procceed();

        ApplyValue(_movement.CalculateMovement(velocity));
        velocity = _gravity.ApplyGravity(velocity);
        
        _fsm.OnLogic();

        _controller.Move(velocity * Time.deltaTime);
    }

    public void ApplyValue(Vector3 value)
    {
        velocity += value;
    }
}
