using UnityEngine;

[System.Serializable]
public class RaySphereSensor : ISensorCaster
{
    private Vector3 _offset;
    private Vector3 _direction;
    private readonly float _radius;

    private RaycastHit _hit;

    public RaySphereSensor(Vector3 offset, Vector3 direction, float radius)
    {
        _offset = offset;
        _direction = direction;
        _radius = radius;
    }

    public float GetDistance()
    {
        return _hit.distance - _offset.magnitude + _radius;
    }

    public Vector3 GetNormal()
    {
        return _hit.normal;
    }

    public Vector3 GetPoint()
    {
        return _hit.point;
    }

    public bool Shoot(Vector3 position)
    {
        return Physics.SphereCast(position + _offset, _radius, _direction, out _hit);
    }
}