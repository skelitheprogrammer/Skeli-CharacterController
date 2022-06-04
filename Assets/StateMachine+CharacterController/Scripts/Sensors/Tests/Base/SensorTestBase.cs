using UnityEngine;

internal abstract class SensorTestBase
{
    protected SensorData _data;
    protected ISensorCaster _sensor;
    protected bool _isHit;

    protected abstract Vector3 ShootPosition { get; set; }

    public abstract void CheckHit();
    public abstract void CheckDistance();
    public abstract void CheckPoint();
    public abstract void CheckNormal();

}
