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

    [Inject] private readonly StateMachineContext _fsm;

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
                    ToggleMovementStyle();
                    _animation.SetIsStrafing(false);
                })
                .WithTick(() =>
                {
                    _jump.CalculateCanJump();
                })
            .Build();

        StateMachine strafeMovementSM = stateMachineBuilder.Begin("Strafe")
            .BuildLogic()
                .WithEnter(() =>
                {
                    ToggleMovementStyle();
                    _animation.SetIsStrafing(true);
                })
            .Build();

        State jumpingS = stateBuilder.Begin("Jumping")
            .BuildLogic()
                .WithEnter(() =>
                {
                    SetSpeed(_jump.CalculateJumpForce());
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
                    AddForce(_gravity.ApplyGravity());
                    _animation.SetGravity(_velocity.y);
                })
                .WithExit(() =>
                {
                    _animation.SetFreeFall(false);
                })
            .Build();

        _fsm.AddState(groundedSM);
        _fsm.AddState(fallingSM);

        groundedSM.AddState(freeformMovementSM);
        groundedSM.AddState(strafeMovementSM);

        freeformMovementSM.AddState(jumpingS);

        _fsm.AddTransition(new Transition(groundedSM, fallingSM, () => !_groundChecker.GroundCheck()));
        _fsm.AddTransition(new Transition(fallingSM, groundedSM, () => _groundChecker.GroundCheck()));

        groundedSM.AddTransition(new Transition(freeformMovementSM, () => _input.SwitchMode));
        groundedSM.AddTransition(new Transition(strafeMovementSM, () => _input.SwitchMode));

        freeformMovementSM.AddTransition(new Transition(freeformMovementSM, jumpingS, () => _jump.CanJump && _input.IsJumped));
        //freeformMovementSM.AddTransition(new Transition(jumpingS, freeformMovementSM));

        groundedSM.SetStartState(freeformMovementSM);
        _fsm.Init(groundedSM);

    }

    private void Update()
    {
        _fsm.UpdateState();

        AddForce(_movementController.CalculateSpeed(_velocity));
        SetRotation(_rotationController.CalculatePlayerRotation());

        _animation.SetHorizontalSpeed(_input.MoveInput.x);
        _animation.SetVerticalSpeed(_input.MoveInput.y);
        _animation.SetSpeed(_input.MoveInput.magnitude);

        _animation.SetAngle(_direction.Angle);

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void AddForce(Vector3 value)
    {
        if (value == Vector3.zero) return;

        _velocity += value;
    }

    private void SetSpeed(Vector3 value)
    {
        if (_velocity == value) return;

        _velocity = value;
    }

    private void SetRotation(Quaternion value)
    {
        transform.rotation = value;
    }

    private void ToggleMovementStyle()
    {
        _rotationController.ToggleRotation();
        _movementController.ToggleMovement();
    }

}