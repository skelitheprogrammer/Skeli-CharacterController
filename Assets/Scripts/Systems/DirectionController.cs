using TMPro;
using UnityEngine;
using Zenject;

public class DirectionController
{
    [Inject] private CharacterStateData _data;
    [Inject(Id = Constants.MAINCAMERA)] private Transform _camera;
    [Inject] private InputReader _input;

    public void ConfigureDirections()
    {
        var normal = _data.normal;
        var inputDirection = _input.MoveInputDirection;
        var moveInput = _input.MoveInput;
        var transform = _data.playerTransform;

        ref var lookSlopeVector = ref _data.lookSlopeVector;
        ref var slopeVector = ref _data.slopeVector;
        ref var jumpVector = ref _data.jumpVector;
        ref var cameraSlopeVector = ref _data.cameraSlopeVector;
        ref var playerDirection = ref _data.playerDirection;

        var forward = new Vector3(_camera.forward.x, 0, _camera.forward.z);
        var right = new Vector3(_camera.right.x, 0, _camera.right.z);
        var cameraDirection = (forward * moveInput.y + right * moveInput.x).normalized;

        lookSlopeVector = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
        cameraSlopeVector = Vector3.ProjectOnPlane(cameraDirection, normal).normalized;
        slopeVector = Vector3.ProjectOnPlane(Vector3.down, normal).normalized;
        jumpVector = (Vector3.up + lookSlopeVector * inputDirection.magnitude).normalized;
        playerDirection = cameraSlopeVector * moveInput.magnitude;
        _data.lastDirection = playerDirection;
    }
}

