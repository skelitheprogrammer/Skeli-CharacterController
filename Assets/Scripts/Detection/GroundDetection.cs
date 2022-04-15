using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [field: SerializeField] public Vector3 Offset { get; private set; }
    [field: SerializeField] public float Distance { get; private set; }
    [field: SerializeField] public float Radius { get; private set; }

    public Vector3 GroundNormal { get; private set; }
    public float Angle { get; private set; }
    public bool IsDetected { get; private set; }

    private void Update()
    {
        Detect();
    }

    public void Detect()
    {
        IsDetected = Physics.SphereCast(transform.position + Offset, Radius, Vector3.down, out var hit, Distance);

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
