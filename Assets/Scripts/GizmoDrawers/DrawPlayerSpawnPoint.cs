using UnityEngine;

public class DrawPlayerSpawnPoint : GizmosBase
{
    [SerializeField] private bool _disableOnStart;

    [SerializeField] private Color _color;

    [Min(1)]
    [SerializeField] private float _height;
    [Min(0)]
    [SerializeField] private float _radius;

    [Space(10f)]
    [SerializeField] private bool _arrow;
    [SerializeField] private Color _arrowColor;
    [SerializeField] private Vector3 _arrowOffset;
    [Min(0)]
    [SerializeField] private float _arrowLength;
    [Min(.1f)]
    [SerializeField] private float _arrowHeadLength;
    [SerializeField] private float _arrowHeadAngle;

    private void Start()
    {
        if (_disableOnStart)
        {
            _enabled = false;
        }
    }

    protected override void DrawGizmo()
    {
        if (!_enabled) return;

        Gizmos.color = _color;
        var position = transform.position + Vector3.up * (_height / 2 - _radius) + Vector3.up * _radius;
        GizmosExtensions.DrawWireCapsule(position, _height, _radius);

        if (!_arrow) return;

        Gizmos.color = _arrowColor;
        DrawArrow.ForGizmo(position + _arrowOffset, transform.forward * _arrowLength, _arrowHeadLength, _arrowHeadAngle, 1f);
    }
}
