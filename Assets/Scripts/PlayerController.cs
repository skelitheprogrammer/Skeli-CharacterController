using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
	[Inject] private readonly GroundCheckController _groundCheck;
	[Inject] private readonly DirectionController _directionController;

	[Inject] private readonly GravitySystem _gravity;
	[Inject] private readonly PlayerJumpSystem _jump;
	[Inject] private readonly PlayerSimpleMovementSystem _movement;
	[Inject] private readonly OriginRotationSystem _originRotation;
	[Inject] private readonly PlayerRotationSystem _playerRotation;

	[Inject] private readonly InputReader _input;

	private StateMachine _fsm;

	[Inject] private CharacterStateData _data;
	[Inject] private CharacterController _controller;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;

		var stateMachineBuilder = new StateMachine.StateMachineBuilder();
		var stateBuilder = new State.StateBuilder();

		_fsm = stateMachineBuilder.Begin().Build();

		StateMachine grounded = stateMachineBuilder.Begin().BuildEnter(() => _data.velocity.y = _gravity.SetGroundedGravity()).Build();
		State isJumping = stateBuilder.Begin().BuildEnter(() => ApplyValue(_jump.CalculateJumpForce())).Build();
        StateMachine isFalling = stateMachineBuilder.Begin()
            .BuildLogic(() =>
            {
				ApplyValue(_gravity.ApplyGravity());
				_data.playerDirection = (_data.lastDirection + _data.playerDirection);
                _data.neededAccel.y = 0;
            }).Build();

        _fsm.AddState(grounded);
        grounded.AddState(isJumping);
        grounded.SetActiveState(isJumping);
        _fsm.AddState(isFalling);

        _fsm.AddTransition(new Transition(grounded, isJumping, (condition) => _data.isGrounded && _input.IsJumped));
        _fsm.AddTransition(new Transition(isJumping, isFalling, (condition) => !_data.isGrounded && _data.velocity.y < 0));
        _fsm.AddTransition(new Transition(grounded, isFalling, (condition) => !_data.isGrounded && _data.velocity.y < 0));
        _fsm.AddTransition(new Transition(isFalling, grounded, (condition) => _data.isGrounded));

        _fsm.SetActiveState(grounded);

    }

	private void Update()
	{
		_groundCheck.GroundCheck();
		_directionController.ConfigureDirections();

		ApplyValue(_movement.CalculateMovement());

		_playerRotation.CharacterRotate();

        _fsm.DoLogic();

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
