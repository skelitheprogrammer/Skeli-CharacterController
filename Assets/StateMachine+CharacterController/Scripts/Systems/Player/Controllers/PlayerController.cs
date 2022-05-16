using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
	[Inject] private readonly GroundCheckController _groundChecker;
	[Inject] private readonly DirectionController _direction;

	[Inject] private readonly GravitySystem _gravity;
	[Inject] private readonly PlayerJumpControllerBase _jump;
	[Inject] private readonly PlayerMovementControllerBase _movementController;
	[Inject] private readonly CameraControllerBase _cameraController;
	[Inject] private readonly PlayerRotationControllerBase _playerRotationController;

	[Inject] private readonly InputReader _input;

	[Inject] private readonly CharacterController _controller;
	[Inject] private readonly PlayerAnimationSync _animation;
	
	[Inject] private readonly StateMachineContext _fsm;

	[SerializeField] private Vector3 _velocity;
	public Vector3 Velocity => _velocity;

	private void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;

		var stateMachineBuilder = new StateMachine.StateMachineBuilder();
		var stateBuilder = new State.StateBuilder();

		StateMachine movementSM = stateMachineBuilder.Begin("Movement")
			.BuildLogic(() => 
			{
				_jump.CalculateCanJump();

				AddForce(_movementController.CalculateSpeed(_velocity));

				transform.rotation = _playerRotationController.CalculatePlayerRotation();
				AddForce(_gravity.ApplyGravity());
				
				_animation.SetHorizontalSpeed(_velocity.magnitude);
				_animation.SetVerticalSpeed(_velocity.y);
			})
			.Build();

		StateMachine groundedSM = stateMachineBuilder.Begin("Grounded")
			.BuildEnter(() => 
			{
				SetSpeed(Vector3.zero);
				_gravity.Toggle(false);
				_animation.SetIsGrounded(true);
			})
			.BuildLogic(() =>
			{
				_velocity.y = 0;
			})
			.BuildExit(() => 
			{
				_animation.SetIsGrounded(false);
			})
			.Build();

		State jumpingS = stateBuilder.Begin("Jumping")
			.BuildEnter(() =>
			{
				_animation.TriggerJump();
				SetSpeed(_jump.CalculateJumpForce());		
			})
			.Build();

		StateMachine fallingSM = stateMachineBuilder.Begin("Falling")
			.BuildEnter(() => 
			{
				_gravity.Toggle(true);
				_animation.SetFreeFall(true);
			})
			.BuildExit(() => 
			{
				_animation.SetFreeFall(false);
			})
			.Build();


        _fsm.AddState(movementSM);

		movementSM.AddState(groundedSM);
		movementSM.AddState(fallingSM);

		groundedSM.AddState(jumpingS);

		movementSM.AddTransition(new Transition(groundedSM, fallingSM, () => !_groundChecker.GroundCheck()));
		movementSM.AddTransition(new Transition(fallingSM, groundedSM, () => _groundChecker.GroundCheck() && movementSM.ActiveState != jumpingS));

		movementSM.AddTransition(new Transition(groundedSM, jumpingS, () => _jump.CanJump && _input.IsJumped));

		_fsm.Init(movementSM);

	}

	private void Update()
	{
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

}
