using UnityEngine;
using Zenject;

public class GroundCheckController : GameSystem
{
    [Inject] private GroundCheckData _groundCheckData;
    [Inject] private CharacterStateData _stateData;

    public void GroundCheck()
    {
        if (!_enabled) return;

        var transform = _stateData.playerTransform;
        var offset = _groundCheckData.SphereOffset;
        ref var isGrounded = ref _stateData.isGrounded;

        isGrounded = Physics.CheckSphere(transform.position + offset, _groundCheckData.Radius);

    }
}
