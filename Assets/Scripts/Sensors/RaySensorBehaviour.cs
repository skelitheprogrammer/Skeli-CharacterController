using UnityEngine;

public class RaySensorBehaviour : MonoBehaviour
{
    [field: SerializeField] public Vector3 Direction { get; private set; }
   
    public RaySensor Sensor { get; private set; }

    public bool IsHit { get; private set; }

    private void Awake()
    {
        Sensor = new(Direction);
    }

    private void Update()
    {
        IsHit = Sensor.Shoot(transform.position);
    }

}