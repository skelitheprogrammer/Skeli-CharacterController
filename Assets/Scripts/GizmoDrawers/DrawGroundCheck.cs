using UnityEngine;
using Zenject;

public class DrawGroundCheck : GizmosBase
{
    [SerializeField] private Color _isGroundedColor;
    [SerializeField] private Color _falseColor;

    [SerializeField] private GroundCheckDataSO _groundCheckData;
    [Inject] private CharacterStateData _characterStateData;


    protected override void DrawGizmo()
    {
        if (_groundCheckData == null) return;

        var offset = _groundCheckData.Data.Offset;
        var position = transform.position;
        var radius = _groundCheckData.Data.Radius;
        var length = _groundCheckData.Data.Length;

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


        var finPos = position + offset;
        Gizmos.DrawRay(finPos, Vector3.down * length);
        Gizmos.DrawWireSphere(finPos + Vector3.down * length, radius);
    }
}
