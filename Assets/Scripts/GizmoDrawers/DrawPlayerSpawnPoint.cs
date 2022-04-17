using UnityEngine;

public class DrawPlayerSpawnPoint : GizmosBase
{
    [SerializeField] private bool _disableOnStart;

    [SerializeField] private Color _color;

    [Min(1)]
    [SerializeField] private float _height;
    [Min(0)]
    [SerializeField] private float _radius;

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
    }
}
