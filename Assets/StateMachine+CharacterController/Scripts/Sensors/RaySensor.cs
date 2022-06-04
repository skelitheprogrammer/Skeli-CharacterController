using UnityEngine;

[System.Serializable]
public class RaySensor : ISensorCaster
{
    private Vector3 _offset;
    private Vector3 _direction;

    public float Distance { get; private set; }
    public Vector3 Point { get; private set; }
    public Vector3 Normal { get; private set; }

    public RaySensor(Vector3 offset, Vector3 direction)
    {
        _offset = offset;
        _direction = direction;
    }

    public bool Shoot(Vector3 position)
    {
        if (Physics.Raycast(position + _offset, _direction, out var hit))
        {
            Distance = hit.distance;
            Point = hit.point;
            Normal = hit.normal;

            return true;
        }

        return false;
    }
}