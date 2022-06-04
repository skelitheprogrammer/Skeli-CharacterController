using System;
using UnityEngine;

public class SensorBehaviour : MonoBehaviour
{
    [field: SerializeField] public SensorCreatorBase SensorSO { get; private set; }

    private void Awake()
    {
        if (SensorSO == null)
        {
            throw new NullReferenceException($"Attach {SensorSO.GetType()} to {name}");
        }
    }

    private void Update()
    {
        SensorSO.Sensor.Shoot(transform.position);
    }
}