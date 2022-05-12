using UnityEngine;
using UnityEngine.Animations.Rigging;

public class DrawFootIKGizmo : GizmosBase
{
    [SerializeField] private FootIKController _controller;

    [SerializeField] private FootIKDataSO _data;
    [SerializeField] private Color _raycastColor;

    private Transform[] _footTransform;

    private void Awake()
    {
        _footTransform = _controller.FootTipTransforms;

    }

    protected override void DrawGizmo()
    {
        if (_data == null || _footTransform == null) return;

        Gizmos.color = _raycastColor;

        for (int i = 0; i < 2; i++)
        {
            var pos = _footTransform[i].position + _data.Data.RayOffset;
            Gizmos.DrawSphere(pos, _data.Data.Radius);
            //Gizmos.DrawLine(pos, pos + Vector3.down * (_controller.FootHitPoint[i].y + _data.Data.RayOffset.y));
            Gizmos.DrawSphere(_controller.FootHitPoint[i], _data.Data.Radius);
        }

    }
}
