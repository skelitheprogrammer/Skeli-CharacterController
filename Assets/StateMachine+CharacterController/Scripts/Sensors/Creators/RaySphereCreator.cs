using UnityEngine;

[CreateAssetMenu(menuName = "Data/Sensors/Ray Sphere Sensor")]
public class RaySphereCreator : SensorCreatorBase
{
    [SerializeField] private SensorData _data;
    [SerializeField] private float _radius;

    public override ISensorCaster Sensor
    {
        get
        {
            if (this == null)
            {
                return new RaySphereSensor(_data.Offset, _data.Direction, _radius);
            }

            return Sensor;
        }
    }
}
