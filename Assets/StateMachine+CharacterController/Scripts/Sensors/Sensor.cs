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

    public RaycastHit hit;
    public bool IsHit { get; private set; }

    public bool Shoot(Vector3 position)
    {
        if (Direction == Vector3.zero) throw new InvalidOperationException($"Direction is Vector3.zero in {GetHashCode()}");

        Ray ray = new(position + Offset, Direction);

        switch (Type)
        {
            case SensorType.Ray:
                IsHit = Physics.Raycast(ray, out hit, Mathf.Infinity);
                break;
            case SensorType.Sphere:
                IsHit = Physics.SphereCast(ray, Radius, out hit);
                break;
        }

        hit.distance = Mathf.Abs(hit.distance - Offset.magnitude);
       
        return IsHit;
    }
}
