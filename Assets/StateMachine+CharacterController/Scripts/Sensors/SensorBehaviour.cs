using UnityEngine;

public class SensorBehaviour : MonoBehaviour
{
    [field: SerializeField] public Sensor Sensor { get; private set; }

    private void Update()
    {
        Sensor.Shoot(transform.position);
    }
}
