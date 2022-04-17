using UnityEngine;
using Zenject;

public class DirectionController : ISystem, ITickable
{
    public bool Enabled { get; private set; } = true;
    [Inject] private CharacterStateData _data;
    [Inject] private InputReader _input;

    public void Tick()
    {
        if (!Enabled) return;
        Procceed();
    }

    public void Procceed()
    {
        var normal = _data.normal;
        var inputDirection = _input.MoveInputDirection;
        var transform = _data.Transform;

        ref var lookSlopeVector = ref _data.lookSlopeVector;
        ref var slopeVector = ref _data.slopeVector;
        ref var jumpVector = ref _data.jumpVector;

        lookSlopeVector = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
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

