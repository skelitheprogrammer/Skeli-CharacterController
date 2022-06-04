using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

public class RaySensorTest
{
    private ISensorCaster _sensor;

    private Vector3 _offset = Vector3.zero;
    private Vector3 _direction = new (0, -1f, 0);
    private Vector3 _shootPosition = new (0,1f,0);
    private bool _isHit;

    [SetUp]
    public void Setup()
    {
        Utils.CreatePrimitive(PrimitiveType.Plane);
        _sensor = new RaySensor(_offset, _direction);
        _isHit = _sensor.Shoot(_shootPosition);
    }

    [Test]
    public void CheckHit()
    {
        Assert.AreEqual(true, _isHit);
    }

    [Test]
    public void CheckDistance()
    {
        Assert.AreEqual(1f, _sensor.Distance);
    }

    [Test]
    public void CheckPoint()
    {
        Assert.AreEqual(Vector3.zero, _sensor.Point);
    }

    [Test]
    public void CheckNormal()
    {
        Assert.That(_sensor.Normal, Is.EqualTo(Vector3.up).Using(Vector3ComparerWithEqualsOperator.Instance));
    }
}
