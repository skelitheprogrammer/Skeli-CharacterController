using UnityEngine;
using Zenject;

public class DirectionController : GameSystem
{
    [Inject] private CharacterStateData _data;
    [Inject] private InputReader _input;

    public override void DoLogic()
    {
        if (!_enabled) return;

        var normal = _data.normal;
        var inputDirection = _input.MoveInputDirection;
        var transform = _data.transform;

        ref var lookSlopeVector = ref _data.lookSlopeVector;
        ref var slopeVector = ref _data.slopeVector;
        ref var jumpVector = ref _data.jumpVector;

        lookSlopeVector = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
        slopeVector = Vector3.ProjectOnPlane(Vector3.down, normal).normalized;
        jumpVector = (Vector3.up + (lookSlopeVector * inputDirection.magnitude)).normalized;
    }

}

