using Skeli.StateMachine;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(LocomotionInstaller))]
public class PlayerLocomotion : LocomotionBase
{
    [Inject] private readonly GravitySystem _gravity;

    [Inject] private readonly LocomotionMovementController _movement;
    [Inject] private readonly LocomotionRotationController _rotation;

    [Inject] private readonly InputReader _input;

    [Inject] private readonly CharacterController _controller;
    //private readonly PlayerAnimationController _animation;

    [Inject] private readonly StateMachineContext _context;

    private bool _isGrounded;

    private Vector3 _velocity;
    public override Vector3 Velocity => _velocity;

    private void Awake()
    {

/*
        StateMachine groundedSM = StateMachineBuilder.Begin("Grounded")
            .BuildLogic()
                .WithEnter(() =>
                {
                    //_animation.SetIsGrounded(true);
                })
                .WithExit(() =>
                {
                    //_animation.SetIsGrounded(false);
                });

        StateMachine freeformMovementSM = StateMachineBuilder.Begin("Freeform")
            .BuildLogic()
                .WithEnter(() =>
                {
                    _movement.service.SetModule(_movement.freeform);
                    _rotation.service.SetModule(_rotation.freeform);
                   // _animation.SetIsStrafing(false);
                })
                .WithTick(() =>
                {
                    AddForce(_movement.service.CalculateSpeed());
                });

        StateMachine strafeMovementSM = StateMachineBuilder.Begin("Strafe")
            .BuildLogic()
                .WithEnter(() =>
                {
                    _movement.service.SetModule(_movement.strafe);
                    _rotation.service.SetModule(_rotation.strafe);
                    //_animation.SetIsStrafing(true);
                })
                .WithTick(() =>
                {
                    SetSpeed(_movement.service.CalculateSpeed());
                })
                .WithExit(() =>
                {
                    _movement.service.SetModule(_movement.freeform);
                    _rotation.service.SetModule(_rotation.strafe);
                });

        State jumpingS = StateBuilder.Begin("Jumping")
            .BuildLogic()
                .WithEnter(() =>
                {
                    //AddForce(_jump.MakeJump();
                    //_animation.TriggerJump();
                });

        StateMachine fallingSM = StateMachineBuilder.Begin("Falling")
            .BuildLogic()
                .WithEnter(() =>
                {
                    //_animation.SetFreeFall(true);
                })
                .WithTick(() =>
                {
                    //_animation.SetGravity(_velocity.y);
                })
                .WithExit(() =>
                {
                    //_animation.SetFreeFall(false);
                });

        State airControlS = StateBuilder.Begin("AirControl")
            .BuildLogic()
                .WithEnter(() =>
                {
                    _movement.service.SetModule(_movement.airControl);
                })
                .WithTick(() =>
                {
                    AddForce(_movement.service.CalculateSpeed());
                });

        _context.AddStateMachine(groundedSM);
        _context.AddStateMachine(fallingSM);

        _context.AddTransition(new Transition(groundedSM, fallingSM, () => !_isGrounded));
        _context.AddTransition(new Transition(fallingSM, groundedSM, () => _isGrounded));

        groundedSM.AddState(freeformMovementSM);
        groundedSM.AddState(strafeMovementSM);

        freeformMovementSM.AddState(jumpingS);

        groundedSM.AddTransition(new Transition(freeformMovementSM, () => _input.SwitchMode));
        groundedSM.AddTransition(new Transition(strafeMovementSM, () => _input.SwitchMode));

        freeformMovementSM.AddTransition(new Transition(freeformMovementSM, jumpingS, () => _input.IsJumped));

        fallingSM.AddState(airControlS);

        //fallingSM.AddTransition(new Transition(fallingSM, jumpingS, () => _jump.CanJump && _input.IsJumped));

        freeformMovementSM.SetEntryState(null);
        groundedSM.SetEntryState(freeformMovementSM);
        fallingSM.SetEntryState(airControlS);
        _context.Init(groundedSM);*/

    }

    private void Update()
    {
        //SetRotation(_rotationController.CalculatePlayerRotation());
        //_isGrounded = Utils.CheckDistance(1f, 0.1f);
        //_context.UpdateState();
        //AddForce(Vector3.up * _gravity.ApplyGravity());

        //_animation.SetHorizontalSpeed(_input.MoveInput.x);
        //_animation.SetVerticalSpeed(_input.MoveInput.y);
        //_animation.SetSpeed(_input.MoveInput.magnitude);

        //_animation.SetAngle(_direction.Angle);

        _controller.Move(_velocity * Time.deltaTime);
    }

    public override void AddForce(Vector3 value)
    {
        _velocity += value;
    }

    public override void SetSpeed(Vector3 value)
    {
        _velocity = value;
    }

    private void SetRotation(Quaternion value)
    {
        transform.rotation = value;
    }
}

public abstract class LocomotionBase : MonoBehaviour
{
    public abstract Vector3 Velocity { get; }

    public abstract void AddForce(Vector3 force);

    public abstract void SetSpeed(Vector3 speed);
}