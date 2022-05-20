using UnityEngine;
using Zenject;

public class PlayerLocomotion : MonoBehaviour
{
    [Inject] private readonly GroundCheckController _groundChecker;
    [Inject] private readonly DirectionController _direction;

    [Inject] private readonly GravitySystem _gravity;
    [Inject] private readonly PlayerJumpControllerBase _jump;
    [Inject] private readonly PlayerMovementController _movementController;
    [Inject] private readonly CameraControllerBase _cameraController;
    [Inject] private readonly PlayerRotationController _rotationController;

    [Inject] private readonly InputReader _input;

    [Inject] private readonly CharacterController _controller;
    [Inject] private readonly PlayerAnimationSync _animation;

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
                    _gravity.Toggle(false);
                    _animation.SetIsGrounded(true);
                })
                .WithTick(() =>
                {
                    _velocity.y = 0;
                })
                .WithExit(() =>
                {
                    _animation.SetIsGrounded(false);
                    _rotationController.SetType(MovementType.Freeform);
                })
            .Build();

        StateMachine freeformMovementSM = stateMachineBuilder.Begin("Freeform")
            .BuildLogic()
                .WithEnter(() =>
                {
                    ToggleMovementStyle();
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
                })
            .Build();

        State jumpingS = stateBuilder.Begin("Jumping")
            .BuildLogic()
                .WithEnter(() =>
                {
                    _animation.TriggerJump();
                    SetSpeed(_jump.CalculateJumpForce());
                })
            .Build();

        StateMachine fallingSM = stateMachineBuilder.Begin("Falling")
            .BuildLogic()
                .WithEnter(() =>
                {
                    _gravity.Toggle(true);
                    _animation.SetFreeFall(true);
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
        _fsm.AddTransition(new Transition(fallingSM, groundedSM, () => _groundChecker.GroundCheck() && _fsm.ActiveStateMachine != jumpingS));

        //_fsm.AddTransition(new Transition(groundedSM, jumpingS, () => _jump.CanJump && _input.IsJumped));

        groundedSM.AddTransition(new Transition(groundedSM, freeformMovementSM, () => _input.SwitchMode));
        groundedSM.AddTransition(new Transition(groundedSM, strafeMovementSM, () => _input.SwitchMode));

        freeformMovementSM.AddTransition(new Transition(freeformMovementSM, jumpingS, () => _jump.CanJump && _input.IsJumped));
        freeformMovementSM.AddTransition(new Transition(jumpingS, freeformMovementSM));

        groundedSM.SetStartState(freeformMovementSM);
        _fsm.Init(groundedSM);

    }

    private void Update()
    {
        AddForce(_movementController.CalculateSpeed(_velocity));
        SetRotation(_rotationController.CalculatePlayerRotation());
        AddForce(_gravity.ApplyGravity());

        _animation.SetHorizontalSpeed(_velocity.magnitude);
        _animation.SetVerticalSpeed(_velocity.y);
        _animation.SetAngle(_direction.Angle);

        _fsm.UpdateState();

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void LateUpdate()
    {
        _cameraController.ControlCamera();
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
        //_movementController.ToggleMovement();
    }

}

public enum MovementType
{
    Freeform,
    Strafe
}