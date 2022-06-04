#if UNITY_EDITOR
using UnityEngine;

public class DrawSensor : DrawGizmosBase
{
    [SerializeField] private SensorBehaviour _sensor;
    [SerializeField] private float _rayRadius = .01f;
    [SerializeField] private float _hitRadius = .02f;
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _hitColor;

    protected override void DrawGizmo()
    {
/*        if (_sensor.TryGetComponent<SensorBehaviour>(out var sensor)) return;

        Gizmos.color = _baseColor;

        var pos = transform.position + sensor.SensorSO.Sensor.Offset;
        var direction = sensor.SensorSO.Sensor.Direction * _sensor.Sensor.Distance - _sensor.Sensor.Offset;

        Gizmos.DrawSphere(pos, _rayRadius);

        if (_sensor.Sensor.IsHit)
        {
            Gizmos.color = _hitColor;
            Gizmos.DrawRay(pos, direction);
            Gizmos.DrawSphere(_sensor.Sensor.Point, _hitRadius);
        }
        else
        {
            Gizmos.DrawRay(pos, _sensor.Sensor.Direction * .5f);
        }*/
    }
}
#endif