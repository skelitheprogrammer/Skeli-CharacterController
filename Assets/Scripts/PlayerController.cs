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
	[Inject] private OriginRotationSystem _originRotation;
	[Inject] private PlayerRotationSystem _playerRotation;

	[Inject] private InputReader _input;

	private StateMachine _fsm;

	[Inject] private CharacterStateData _data;
	[Inject] private CharacterController _controller;

	private void Awake()
	{

	}

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		var stateBuilder = new StateBuilder();

		//_fsm = builder/*.Logic(() => ApplyValue(_gravity.ApplyGravity()))*/.Build();
		_fsm = stateBuilder.Build<StateMachine>();
		StateMachine grounded = stateBuilder.Build<StateMachine>();
		State isJumping = stateBuilder.Enter(() => ApplyValue(_jump.CalculateJumpForce())).Build<State>();
		StateMachine isFalling = stateBuilder.Build<StateMachine>();

		_fsm.AddState(grounded);
		grounded.AddState(isJumping);
		_fsm.AddState(isFalling);

		_fsm.AddTransition(new Transition(grounded, isJumping, (condition) => _data.isGrounded && _data.canJump));
		_fsm.AddTransition(new Transition(isJumping, isFalling, (condition) => _data.velocity.y < 0));
		_fsm.AddTransition(new Transition(grounded, isFalling, (condition) => !_data.isGrounded && _data.velocity.y < 0));
		_fsm.AddTransition(new Transition(isFalling, grounded, (condition) => _data.isGrounded));

		_fsm.SetActiveState(grounded);
	}

	private void Update()
	{
		_groundCheck.DoLogic();
		_directionController.DoLogic();

		ApplyValue(_movement.CalculateMovement());

		_playerRotation.DoLogic();

		Debug.Log(_fsm.ActiveState.GetType().Name);
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
