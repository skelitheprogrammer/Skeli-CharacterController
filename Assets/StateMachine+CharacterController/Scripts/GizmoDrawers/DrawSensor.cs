using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DrawSensor : DrawGizmosBase
{
    [SerializeField] private SensorBehaviour _sensor;
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _hitColor;

    protected override void DrawGizmo()
    {
        if (_sensor == null) return;

        Gizmos.color = _baseColor;

        var pos = transform.position + _sensor.Sensor.Offset;
        
        Gizmos.DrawSphere(pos, _sensor.Sensor.Radius);
        Gizmos.DrawRay(pos, _sensor.Sensor.Direction * _sensor.Sensor.hit.distance);
        
        if (_sensor.Sensor.IsHit)
        {
            Gizmos.color = _hitColor;
            Gizmos.DrawSphere(_sensor.Sensor.hit.point, _sensor.Sensor.Radius);
        }
    }
}