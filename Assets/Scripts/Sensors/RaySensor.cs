using UnityEngine;

[System.Serializable]
public class RaySensor : ISensorCaster
{
    private Vector3 _direction;

    private RaycastHit _hit;

    public RaySensor(Vector3 direction)
    {
        _direction = direction;
    }

    public bool Shoot(Vector3 position)
    {
        Ray ray = new(position, _direction);
        return Physics.Raycast(ray, out _hit);
    }

    public float GetDistance()
    {
        return _hit.distance;
    }

    public Vector3 GetNormal()
    {
        return _hit.normal;
    }

    public Vector3 GetPoint()
    {
        return _hit.point;
    }
}
