using UnityEngine;
using Zenject;

public class DrawPlayerVelocityDirection : GizmosBase
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Color _color;
    [Inject] private CharacterStateData _data;

    protected override void DrawGizmo()
    {
        if (_data == null) return;

        var speed = _data.velocity;

        Gizmos.color = _color;
        Gizmos.DrawRay(transform.position + _offset, speed);
    }
}
