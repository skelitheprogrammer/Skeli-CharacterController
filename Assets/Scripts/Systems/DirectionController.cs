using UnityEngine;
using Zenject;

public class DirectionController : ISystem
{
    public bool Enabled { get; private set; }
    [Inject] private CharacterStateData _data;
    [Inject] private InputReader _input;

    public void Procceed()
    {
        var normal = _data.normal;
        var inputDirection = _input.MoveInputDirection;
        var forward = _data.Transform.forward;

        ref var lookSlopeVector = ref _data.lookSlopeVector;
        ref var slopeVector = ref _data.slopeVector;
        ref var jumpVector = ref _data.jumpVector;

        lookSlopeVector = Vector3.ProjectOnPlane(forward, normal).normalized;
        slopeVector = Vector3.ProjectOnPlane(Vector3.down, normal).normalized;
        jumpVector = (Vector3.up + (lookSlopeVector * inputDirection.magnitude)).normalized;
    }

    public void Toggle()
    {
        Enabled = !Enabled;
    }

    public void Toggle(bool state)
    {
        Enabled = state;
    }

}

