﻿using UnityEngine;

[CreateAssetMenu(menuName ="Data/Sensors/Ray Sensor")]
public class RaySensorCreator : SensorCreatorBase
{
    [SerializeField] private SensorData _data;

    public override ISensorCaster Sensor
    {
        get
        {
            if (this == null)
            {
                return new RaySensor(_data.Offset, _data.Direction);
            }

            return Sensor;
        }
    }
}
