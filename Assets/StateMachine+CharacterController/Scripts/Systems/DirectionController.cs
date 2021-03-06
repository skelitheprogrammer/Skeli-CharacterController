using UnityEngine;
using Zenject;

public class DirectionController : GameSystem
{
	[Inject] private readonly GroundCheckData _groundCheckData;
	[Inject] private readonly CharacterStateData _data;
	[Inject] private readonly InputReader _input;

	public void ConfigureDirections()
	{
		if (!_enabled) return;

		var inputDirection = _input.MoveInputDirection;
		var moveInput = _input.MoveInput;
		var transform = _data.playerTransform;
		var camera = _data.camera;

		ref var normal = ref _data.normal;
		ref var playerDirection = ref _data.playerDirection;
		ref var lookSlopeVector = ref _data.lookSlopeVector;
		ref var slopeVector = ref _data.slopeVector;
		ref var jumpVector = ref _data.jumpVector;
		ref var cameraSlopeVector = ref _data.cameraSlopeVector;
		ref var angle = ref _data.slopeAngle;

		var forward = new Vector3(camera.forward.x, 0, camera.forward.z);
		var right = new Vector3(camera.right.x, 0, camera.right.z);
		var cameraDirection = (forward * moveInput.y + right * moveInput.x).normalized;

		Physics.Raycast(transform.position + _groundCheckData.RayOffset, Vector3.down, out var hit, _groundCheckData.Length);

		angle = Vector3.Angle(hit.normal, Vector3.up);

		if (angle > 1)
		{
			normal = hit.normal;
		} 
		else
		{
			normal = Vector3.up;
		}

		slopeVector = Vector3.ProjectOnPlane(Vector3.down, normal).normalized;
		lookSlopeVector = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
		cameraSlopeVector = Vector3.ProjectOnPlane(cameraDirection, normal).normalized;
		jumpVector = (Vector3.up + cameraSlopeVector * inputDirection.magnitude).normalized;
		playerDirection = cameraSlopeVector;

		if (!_data.isGrounded)
		{
			normal = Vector3.zero;
			slopeVector = Vector3.zero;
			lookSlopeVector = Vector3.zero;
			playerDirection = (_data.previousPlayerDirection + playerDirection).normalized;
		}

		_data.previousPlayerDirection = playerDirection;
	}
}