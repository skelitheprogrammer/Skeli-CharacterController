using UnityEngine;

[System.Serializable]
public class RaySphereSensor : ISensorCaster
{
    private Vector3 _offset;
    private Vector3 _direction;
    private float _radius;

    public float Distance { get; private set; }
    public Vector3 Point { get; private set; }
    public Vector3 Normal { get; private set; }

    public RaySphereSensor(Vector3 offset, Vector3 direction, float radius)
    {
        _offset = offset;
        _direction = direction;
        _radius = radius;
    }

    public bool Shoot(Vector3 position)
    {
        if (Physics.SphereCast(position + _offset, _radius, _direction, out var hit))
        {
            Distance = hit.distance - _offset.magnitude + _radius;
            Point = hit.point;
            Normal = hit.normal;

            return true;
        }

        return false;
    }
}