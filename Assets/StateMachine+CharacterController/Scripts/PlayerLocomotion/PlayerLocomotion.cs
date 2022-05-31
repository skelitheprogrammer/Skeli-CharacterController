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

        StateMachine groundedSM = StateMachineBuilder.Begin("Grounded")
            .BuildLogic()
                .WithEnter(() =>
                {
                    SetSpeed(Vector3.zero);
                    _animation.SetIsGrounded(true);
                })
                .WithTick(() =>
                {
                    _velocity.y = _gravity.SetGroundedGravity();
                })
                .WithExit(() =>
                {
                    _animation.SetIsGrounded(false);
                });

        StateMachine freeformMovementSM = StateMachineBuilder.Begin("Freeform")
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
                });

        StateMachine strafeMovementSM = StateMachineBuilder.Begin("Strafe")
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
                });

        State jumpingS = StateBuilder.Begin("Jumping")
            .BuildLogic()
                .WithEnter(() =>
                {
                    _groundChecker.Toggle(false);
                    SetSpeed(_jump.CalculateJumpForce());
                    _animation.TriggerJump();
                })
                .WithExit(() => 
                {
                    _groundChecker.Toggle(true);
                });

        StateMachine fallingSM = StateMachineBuilder.Begin("Falling")
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
                });

        State airControlS = StateBuilder.Begin("AirControl")
            .BuildLogic()
                .WithEnter(() =>
                {
                    _movementController.SetAirControl();
                })
                .WithTick(() =>
                {
                    AddForce(_movementController.CalculateSpeed(_velocity));
                });

        _context.AddStateMachine(groundedSM);
        _context.AddStateMachine(fallingSM);

        _context.AddTransition(new Transition(groundedSM, fallingSM, () => !_groundChecker.GroundCheck()));
        _context.AddTransition(new Transition(fallingSM, groundedSM, () => _groundChecker.GroundCheck()));

        groundedSM.AddState(freeformMovementSM);
        groundedSM.AddState(strafeMovementSM);

        freeformMovementSM.AddState(jumpingS);

        groundedSM.AddTransition(new Transition(freeformMovementSM, () => _input.SwitchMode));
        groundedSM.AddTransition(new Transition(strafeMovementSM, () => _input.SwitchMode));

        freeformMovementSM.AddTransition(new Transition(freeformMovementSM, jumpingS, () => _jump.CanJump && _input.IsJumped));
        //freeformMovementSM.AddTransition(new Transition(jumpingS, freeformMovementSM));

        fallingSM.AddState(airControlS);

        freeformMovementSM.SetEntryState(null);
        groundedSM.SetEntryState(freeformMovementSM);
        fallingSM.SetEntryState(airControlS);
        _context.Init(groundedSM);

    }

    private void Update()
    {
        SetRotation(_rotationController.CalculatePlayerRotation());
        _context.UpdateState();

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