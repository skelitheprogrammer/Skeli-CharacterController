using UnityEngine;

public class GroundDetection
{
    public Vector3 Offset { get; private set; }
    public float Distance { get; private set; }
    public float Radius { get; private set; }

    public Vector3 GroundNormal { get; private set; }
    public float Angle { get; private set; }
    public bool Detected { get; private set; }

    public GroundDetection(Vector3 offset, float distance, float radius)
    {
        Offset = offset;
        Distance = distance;
        Radius = radius;
    }

    public void Detect(Vector3 position)
    {
        Detected = Physics.SphereCast(position + Offset, Radius, Vector3.down, out var hit, Distance);

        Angle = Vector3.Angle(hit.normal, Vector3.up);

        if (Angle < 1)
        {
            GroundNormal = Vector3.up;
        }
        else
        {
            GroundNormal = hit.normal;
        }
    }
}
