using UnityEngine;

#if UNITY_EDITOR
public class DrawPlayerSpawnPoint : DrawGizmosBase
{
    [SerializeField] private bool _disableOnStart;

    [SerializeField] private Color _color;

    [Min(1)]
    [SerializeField] private float _height;
    [Min(0)]
    [SerializeField] private float _radius;

    [Space(10f)]
    [SerializeField] private bool _arrow;
    [SerializeField] private ArrowStruct _arrowSettings;

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
        GizmosUtils.DrawWireCapsule(position, _height, _radius);

        if (!_arrow) return;

        Gizmos.color = _arrowSettings.ArrowColor;
        DrawArrow.ForGizmo(position + _arrowSettings.ArrowOffset, transform.forward * _arrowSettings.ArrowLength, _arrowSettings.ArrowHeadLength, _arrowSettings.ArrowHeadAngle, 1f);
    }
}
#endif
[System.Serializable]
public struct ArrowStruct
{
    public Color ArrowColor;
    public Vector3 ArrowOffset;
    [Min(0)]
    public float ArrowLength;
    [Min(.1f)]
    public float ArrowHeadLength;
    public float ArrowHeadAngle;
}
