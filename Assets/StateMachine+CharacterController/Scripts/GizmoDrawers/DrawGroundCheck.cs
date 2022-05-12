using UnityEngine;
using Zenject;

public class DrawGroundCheck : GizmosBase
{
    [SerializeField] private Color _isGroundedColor;
    [SerializeField] private Color _falseColor;

    [SerializeField] private float _initRadius;
    [SerializeField] private float _hitRadius;

    [SerializeField] private GroundCheckDataSO _groundCheckData;
    [Inject] private readonly GroundCheckController _controller;

    protected override void DrawGizmo()
    {
        if (_groundCheckData == null) return;

        if (_controller != null)
        {
            if (_controller.GroundCheck())
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

        Gizmos.DrawSphere(transform.position + _groundCheckData.Data.RayOffset, _initRadius);
        Gizmos.DrawRay(transform.position + _groundCheckData.Data.RayOffset, Vector3.down * _controller.hit.point.y);
        Gizmos.DrawSphere(_controller.hit.point, _hitRadius);
    }
}
