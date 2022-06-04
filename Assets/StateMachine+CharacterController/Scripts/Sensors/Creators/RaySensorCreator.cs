using UnityEngine;

[CreateAssetMenu(menuName ="Data/Sensors/Ray Sensor")]
public class RaySensorCreator : SensorCreatorBase
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _direction;

    public override ISensorCaster Sensor
    {
        get
        {
            if (this == null)
            {
                return new RaySensor(_offset, _direction);
            }

            return Sensor;
        }
    }
}
