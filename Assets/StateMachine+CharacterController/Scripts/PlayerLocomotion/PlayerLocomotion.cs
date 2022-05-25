using Skeli.StateMachine;
using UnityEngine;
using Zenject;

public class PlayerLocomotion : MonoBehaviour
{
    [Inject] private readonly GroundCheckController _groundChecker;
    [Inject] private readonly DirectionController _direction;

    [Inject] private readonly GravitySystem _gravity;
    [Inject] private readonly PlayerJumpController _jump;
    [Inject] private readonly PlayerMovementController _movementController;
    [Inject] private readonly PlayerRotationController _rotationController;

    [Inject] private readonly InputReader _input;

    [Inject] private readonly CharacterController _controller;
    [Inject] private readonly PlayerAnimationController _animation;

    [Inject] private readonly StateMachineContext _context;

    [SerializeField] private Vector3 _velocity;
    public Vector3 Velocity => _velocity;

    private void Awake()
    {
        var stateMachineBuilder = new StateMachineBuilder();
        var stateBuilder = new StateBuilder();

        StateMachine groundedSM = stateMachineBuilder.Begin("Grounded")
            .BuildLogic()
                .WithEnter(() =>
                {
                    SetSpeed(Vector3.zero);
                    _animation.SetIsGrounded(true);
                })
                .WithTick(() =>
                {
                    if (_groundChecker.GroundCheck())
                    {
                        if (_direction.IsOnSlope)
                        {
                            _velocity.y = _gravity.SetGroundedGravity();
                        }
                    }
                })
                .WithExit(() =>
                {
                    _animation.SetIsGrounded(false);
                })
            .Build();

        StateMachine freeformMovementSM = stateMachineBuilder.Begin("Freeform")
            .BuildLogic()
                .WithEnter(() =>
                {
                    _rotationController.SetFreeForm();
                    _movementController.SetFreeForm();
                    _animation.SetIsStrafing(false);
                })
                .WithTick(() =>
                {
                    _jump.CalculateCanJump();
                    AddForce(_movementController.CalculateSpeed(_velocity));
                })
            .Build();

        StateMachine strafeMovementSM = stateMachineBuilder.Begin("Strafe")
            .BuildLogic()
                .WithEnter(() =>
                {
                    _rotationController.SetStrafe();
                    _movementController.SetStrafe();
                    _animation.SetIsStrafing(true);
                })
                .WithTick(() =>
                {
                    SetSpeed(_movementController.CalculateSpeed(_velocity));
                })
            .Build();

        State jumpingS = stateBuilder.Begin("Jumping")
            .BuildLogic()
                .WithEnter(() =>
                {
                    SetSpeed(Vector3.zero);
                    AddForce(_jump.CalculateJumpForce());
                    Debug.Log($"{Velocity} {Velocity.magnitude}");
                    _animation.TriggerJump();
                })
            .Build();

        StateMachine fallingSM = stateMachineBuilder.Begin("Falling")
            .BuildLogic()
                .WithEnter(() =>
                {
                    _animation.SetFreeFall(true);
                })
                .WithTick(() =>
                {
                    AddForce(Vector3.up * _gravity.ApplyGravity());
                    _animation.SetGravity(_velocity.y);
                })
                .WithExit(() =>
                {
                    _animation.SetFreeFall(false);
                })
            .Build();

        State airControlS = stateBuilder.Begin("AirControl")
            .BuildLogic()
                .WithEnter(() =>
                {
                    _movementController.SetAirControl();
                })
                .WithTick(() =>
                {
                    //AddForce(_movementController.CalculateSpeed(_velocity));
                })
            .Build();

        _context.AddState(groundedSM);
        _context.AddState(fallingSM);

        _context.AddTransition(new Transition(groundedSM, fallingSM, () => !_groundChecker.GroundCheck()));
        _context.AddTransition(new Transition(fallingSM, groundedSM, () => _groundChecker.GroundCheck()));

        groundedSM.AddState(freeformMovementSM);
        groundedSM.AddState(strafeMovementSM);

        freeformMovementSM.AddState(jumpingS);

        groundedSM.AddTransition(new Transition(freeformMovementSM, () => _input.SwitchMode));
        groundedSM.AddTransition(new Transition(strafeMovementSM, () => _input.SwitchMode));

        freeformMovementSM.AddTransition(new Transition(freeformMovementSM, jumpingS, () => _jump.CanJump && _input.IsJumped));

        fallingSM.AddState(airControlS);

        groundedSM.SetEntryState(freeformMovementSM);
        fallingSM.SetEntryState(airControlS);
        _context.Init(groundedSM);

    }

    private void Update()
    {
        _context.UpdateState();

        SetRotation(_rotationController.CalculatePlayerRotation());

        _animation.SetHorizontalSpeed(_input.MoveInput.x);
        _animation.SetVerticalSpeed(_input.MoveInput.y);
        _animation.SetSpeed(_input.MoveInput.magnitude);

        _animation.SetAngle(_direction.Angle);

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void AddForce(Vector3 value)
    {
        _velocity += value;
    }

    private void SetSpeed(Vector3 value)
    {
        _velocity = value;
    }

    private void SetRotation(Quaternion value)
    {
        transform.rotation = value;
    }
}