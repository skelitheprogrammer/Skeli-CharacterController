using UnityEngine;

public abstract class SensorCreatorBase : ScriptableObject
{
    public abstract ISensorCaster Sensor { get; }
}
