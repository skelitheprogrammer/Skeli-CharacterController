using UnityEngine;
using Zenject;

public class DirectionController
{
    [Inject] private readonly GroundCheckData _groundCheckData;
    [Inject] private readonly CharacterStateData _data;
    [Inject] private readonly InputReader _input;

    public void ConfigureDirections()
    {
        var inputDirection = _input.MoveInputDirection;
        var moveInput = _input.MoveInput;
        var transform = _data.playerTransform;
        var camera = _data.camera;

        ref var normal = ref _data.normal;
        ref var lookSlopeVector = ref _data.lookSlopeVector;
        ref var slopeVector = ref _data.slopeVector;
        ref var jumpVector = ref _data.jumpVector;
        ref var cameraSlopeVector = ref _data.cameraSlopeVector;
        ref var playerDirection = ref _data.playerDirection;
        ref var angle = ref _data.slopeAngle;

        var forward = new Vector3(camera.forward.x, 0, camera.forward.z);
        var right = new Vector3(camera.right.x, 0, camera.right.z);
        var cameraDirection = (forward * moveInput.y + right * moveInput.x).normalized;

        Physics.SphereCast(transform.position + _groundCheckData.Offset, _groundCheckData.Radius, Vector3.down, out var hit, _groundCheckData.Length);
        
        angle = Vector3.Angle(hit.normal, Vector3.up);

        normal = hit.normal;

        lookSlopeVector = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
        cameraSlopeVector = Vector3.ProjectOnPlane(cameraDirection, normal).normalized;
        slopeVector = Vector3.ProjectOnPlane(Vector3.down, normal).normalized;
        jumpVector = (Vector3.up + lookSlopeVector * inputDirection.magnitude).normalized;
        playerDirection = cameraSlopeVector * moveInput.magnitude;

        if (!_data.isGrounded)
        {
            playerDirection = (_data.lastDirection + playerDirection).normalized;
        }

        _data.lastDirection = playerDirection;
    }
}

