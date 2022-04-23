using FSM;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Inject] private GroundCheckController _groundCheck;
    [Inject] private DirectionController _directionController;

    [Inject] private GravitySystem _gravity;
    [Inject] private PlayerJumpSystem _jump;
    [Inject] private PlayerSimpleMovementSystem _movement;
    [Inject] private OriginRotationSystem _originRotation;
    [Inject] private PlayerRotationSystem _playerRotation;

    [Inject] private InputReader _input;

    [Inject] private CharacterStateData _data;
    [Inject] private StateMachine _fsm;
    [Inject] private CharacterController _controller;

    private void Awake()
    {
        _fsm.AddState("IsGrounded", new State((enter) => _data.canJump = true));
        _fsm.AddState("IsJumping", new State((enter) => ApplyValue(_jump.CalculateJumpForce())));
        _fsm.AddState("IsFalling", new State((enter) => _data.canJump = false));

        _fsm.AddTransition(new Transition("IsGrounded", "IsJumping", (condition) => _input.IsJumped && _data.canJump));
        _fsm.AddTransition(new Transition("IsJumping", "IsGrounded", (condition) => _data.isGrounded));

        _fsm.AddTransition(new Transition("IsGrounded", "IsFalling", (condition) => _data.velocity.y <= 0 && !_data.isGrounded));
        _fsm.AddTransition(new Transition("IsFalling", "IsGrounded", (condition) => _data.isGrounded));
        _fsm.AddTransition(new Transition("IsJumping", "IsFalling", (condition) => _data.velocity.y <= 0 && !_data.isGrounded));

        _fsm.SetStartState("IsGrounded");
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _fsm.Init();
    }

    private void Update()
    {
        _groundCheck.DoLogic();
        _directionController.DoLogic();

        _gravity.ApplyGravity();
        ApplyValue(_movement.CalculateMovement());


        _playerRotation.DoLogic();

        _fsm.OnLogic();
        _controller.Move(_data.velocity * Time.deltaTime);
    }

    private void LateUpdate()
    {
        _originRotation.DoLogic();
    }

    public void ApplyValue(Vector3 value)
    {
        _data.velocity += value;
    }
}
