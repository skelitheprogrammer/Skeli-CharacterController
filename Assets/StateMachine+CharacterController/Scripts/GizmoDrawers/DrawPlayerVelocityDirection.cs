using UnityEngine;
using Zenject;

public class DrawPlayerVelocityDirection : GizmosBase
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Color _color;
    [Inject] private PlayerController _controller;

    protected override void DrawGizmo()
    {
        if (_controller == null) return;

        var speed = _controller.Velocity;

        Gizmos.color = _color;
        Gizmos.DrawRay(transform.position + _offset, speed);
    }
}
