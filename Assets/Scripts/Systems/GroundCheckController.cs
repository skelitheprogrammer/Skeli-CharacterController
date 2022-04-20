using UnityEngine;
using Zenject;

public class GroundCheckController : GameSystem
{
    [Inject] private GroundCheckData _groundCheckData;
    [Inject] private CharacterStateData _stateData;

    public override void DoLogic()
    {
        if (!_enabled) return;

        var transform = _stateData.transform;
        ref var normal = ref _stateData.normal;
        ref var angle = ref _stateData.slopeAngle;
        ref var isGrounded = ref _stateData.isGrounded;

        isGrounded = Physics.SphereCast(transform.position + _groundCheckData.Offset, _groundCheckData.Radius, Vector3.down,out var hit, _groundCheckData.Length);
        angle = Vector3.Angle(hit.normal, Vector3.up);

        if (angle == 0)
        {
            normal = Vector3.up;
        }
        else
        {
            normal = hit.normal;
        }
    }
}
