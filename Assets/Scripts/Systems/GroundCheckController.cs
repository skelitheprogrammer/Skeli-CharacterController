using UnityEngine;
using Zenject;

public class GroundCheckController
{
    [Inject] private GroundCheckData _groundCheckData;
    [Inject] private CharacterStateData _stateData;

    public void GroundCheck()
    {
        var transform = _stateData.playerTransform;
        var offset = _groundCheckData.SphereOffset;
        ref var isGrounded = ref _stateData.isGrounded;

        isGrounded = Physics.CheckSphere(transform.position + offset, _groundCheckData.Radius);

    }
}
