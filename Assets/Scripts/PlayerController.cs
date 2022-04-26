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
	
	private StateMachine _fsm;

    private void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;

		var stateMachineBuilder = new StateMachine.StateMachineBuilder();
		var stateBuilder = new State.StateBuilder();

		_fsm = stateMachineBuilder.Begin("Root")
			.Build();
		StateMachine grounded = stateMachineBuilder.Begin("Grounded")
			.BuildLogic(() =>
			{
				_data.velocity.y = _gravity.SetGroundedGravity();
			})
			.BuildExit(() =>
			{
				_data.velocity.y = 0;
			})
			.Build();
		State isJumping = stateBuilder.Begin("IsJumping")
			.BuildEnter(() => _data.velocity = _jump.CalculateJumpForce())
			.Build();
        StateMachine isFalling = stateMachineBuilder.Begin("IsFalling")
            .BuildLogic(() =>
            {
				AddForce(_gravity.ApplyGravity());
				_data.neededAccel.y = 0;
			})
			.Build();

        _fsm.AddState(grounded);
        grounded.AddState(isJumping);
        grounded.SetActiveState(isJumping);
        _fsm.AddState(isFalling);

        _fsm.AddTransition(new Transition(grounded, isJumping, (condition) => _data.isGrounded && _input.IsJumped));
        _fsm.AddTransition(new Transition(isJumping, isFalling, (condition) => !_data.isGrounded));
        _fsm.AddTransition(new Transition(grounded, isFalling, (condition) => !_data.isGrounded));
        _fsm.AddTransition(new Transition(isFalling, grounded, (condition) => _data.isGrounded));

        _fsm.SetActiveState(grounded);

    }

	private void Update()
	{
		_groundCheck.GroundCheck();
		_directionController.ConfigureDirections();

		AddForce(_movement.CalculateMovement());

		_data.playerTransform.rotation = _playerRotation.CalculateRotationAngle();


		_fsm.DoLogic();
		_controller.Move(_data.velocity * Time.deltaTime);
	}

	private void LateUpdate()
	{
		_originRotation.DoLogic();
	}
	private void AddForce(Vector3 value)
	{
		_data.velocity += value;
	}

}
