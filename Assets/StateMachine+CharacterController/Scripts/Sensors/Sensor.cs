using System;
using UnityEngine;

[Serializable]
public class Sensor
{
    public enum SensorType
    {
        Ray,
        Sphere
    }

    [field : SerializeField] public Vector3 Offset { get; private set;}

    [field: SerializeField] public Vector3 Direction { get; private set; }

    [field: SerializeField] public SensorType Type { get; private set; }
    
    [field: SerializeField] public float Radius { get; private set; }

    private RaycastHit _hit;

    public bool IsHit { get; private set; }
    public float Distance { get; private set; }
    public Vector3 Point => _hit.point;
    public Vector3 Normal => _hit.normal;

    public bool Shoot(Vector3 position)
    {
        if (Direction == Vector3.zero) throw new InvalidOperationException($"Direction is Vector3.zero in {GetHashCode()}");

        switch (Type)
        {
            case SensorType.Ray:
                IsHit = Physics.Raycast(position + Offset,Direction, out _hit, Mathf.Infinity);
                break;
            case SensorType.Sphere:
                IsHit = Physics.SphereCast(position + Offset + Vector3.up * Radius, Radius, Direction, out _hit);
                break;
        }

        Distance = Mathf.Abs(Offset.magnitude - _hit.distance);
        Debug.Log($"{Offset.magnitude} - {_hit.distance} = {Distance}");
        return IsHit;
    }
}
