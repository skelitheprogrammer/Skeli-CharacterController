using UnityEngine;

#if UNITY_EDITOR
public class DrawPlayerVelocityDirection : DrawGizmosBase
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Color _color;
    private PlayerLocomotion _controller;

    public override void DrawGizmo()
    {
        if (_controller == null) return;

        var speed = _controller.Velocity;

        Gizmos.color = _color;
        Gizmos.DrawRay(transform.position + _offset, speed);
    }
}
#endif