using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SensorData
{
    [field: SerializeField] public Vector3 Offset { get; private set; }
    [field: SerializeField] public Vector3 Direction { get; private set; }

    public SensorData(Vector3 offset, Vector3 direction)
    {
        Offset = offset;
        Direction = direction;
    }
}
