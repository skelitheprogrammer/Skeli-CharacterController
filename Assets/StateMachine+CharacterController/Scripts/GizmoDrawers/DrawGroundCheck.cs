using UnityEngine;
using Zenject;

public class DrawGroundCheck : GizmosBase
{
    [SerializeField] private Color _isGroundedColor;
    [SerializeField] private Color _falseColor;

    [SerializeField] private GroundCheckDataSO _groundCheckData;
    [Inject] private readonly CharacterStateData _characterStateData;

    protected override void DrawGizmo()
    {
        if (_groundCheckData == null) return;

        if (_characterStateData != null)
        {
            if (_characterStateData.isGrounded)
            {
                Gizmos.color = _isGroundedColor;
            }
            else
            {
                Gizmos.color = _falseColor;
            }
        }
        else
        {
            Gizmos.color = _falseColor;
        }

        Gizmos.DrawRay(transform.position + _groundCheckData.Data.RayOffset, Vector3.down * _groundCheckData.Data.Length);
        Gizmos.DrawWireSphere(transform.position + _groundCheckData.Data.SphereOffset, _groundCheckData.Data.Radius);
    }
}
