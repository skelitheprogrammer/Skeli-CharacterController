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

	[Inject] private readonly CharacterStateData _data;
	[Inject] private readonly CharacterController _controller;
	
	private StateMachineContext _fsm;

	private void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;

		var stateMachineBuilder = new StateMachine.StateMachineBuilder();
		var stateBuilder = new State.StateBuilder();

		_fsm = new StateMachineContext();

		StateMachine grounded = stateMachineBuilder.Begin("Grounded")
			.BuildLogic(() =>
			{
				_data.velocity.y = _gravity.SetGroundedGravity();
			})
			.Build();

		State isJumping = stateBuilder.Begin("Jumping")
			.BuildEnter(() =>
			{
				_groundCheck.Toggle(false);
				_data.isGrounded = false;
				_data.velocity = _jump.CalculateJumpForce();
			})
			.BuildExit(() => 
			{
				_groundCheck.Toggle(true);
			})
			.Build();

		StateMachine isFalling = stateMachineBuilder.Begin("Falling")
			.BuildLogic(() =>
			{
				AddForce(_gravity.ApplyGravity());
				_data.neededAccel.y = 0;
			})
			.Build();

		_fsm.AddState(grounded);
		grounded.AddState(isJumping);
		_fsm.AddState(isFalling);

		grounded.AddTransition(new Transition(grounded, isJumping, () => _data.isGrounded && _input.IsJumped));
		grounded.AddTransition(new Transition(isJumping, grounded, () => _data.isGrounded));
		_fsm.AddTransition(new Transition(isJumping, isFalling, () => !_data.isGrounded));
		_fsm.AddTransition(new Transition(grounded, isFalling, () => !_data.isGrounded));
		_fsm.AddTransition(new Transition(isFalling, grounded, () => _data.isGrounded));

		_fsm.Init();

	}

	private void Update()
	{
		_groundCheck.GroundCheck();
		_directionController.ConfigureDirections();

		_fsm.UpdateState();

		AddForce(_movement.CalculateMovement());
		_data.playerTransform.rotation = _playerRotation.CalculateRotationAngle();

		_controller.Move(_data.velocity * Time.deltaTime);
	}

	private void LateUpdate()
	{
		_originRotation.OriginRotation();
	}

	private void AddForce(Vector3 value)
	{
		_data.velocity += value;
	}

}
