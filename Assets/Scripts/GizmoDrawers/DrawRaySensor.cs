using UnityEngine;

[RequireComponent(typeof(RaySensorBehaviour))]
public class DrawRaySensor : DrawGizmosBase
{
    [SerializeField] private RaySensorBehaviour _sensor;

    [SerializeField] private float _radius;

    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _hitColor;

    public override void DrawGizmo()
    {
        if (_sensor == null) return;

        Gizmos.color = _defaultColor;

        Gizmos.DrawSphere(transform.position, _radius);

        if (!_sensor.IsHit)
        {
            Gizmos.DrawRay(transform.position, _sensor.Direction);
        }
        else
        {
            Gizmos.color = _hitColor;
            var point = _sensor.Sensor.GetPoint();
            Gizmos.DrawLine(transform.position, point);
            Gizmos.DrawSphere(_sensor.Sensor.GetPoint(), _radius);
        }
    }

}
