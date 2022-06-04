using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

internal class RaySensorWithoutOffset : SensorTestBase
{
    protected override Vector3 ShootPosition { get; set; } = Vector3.up;

    [SetUp]
    public void Setup()
    {
        _data = new SensorData(Vector3.zero, Vector3.down);
        Utils.CreatePrimitive(PrimitiveType.Plane);
        _sensor = new RaySensor(_data.Offset, _data.Direction);
        _isHit = _sensor.Shoot(ShootPosition);
    }

    [Test]
    public override void CheckHit()
    {
        Assert.AreEqual(true, _isHit);
    }

    [Test]
    public override void CheckDistance()
    {
        Assert.AreEqual(1f, _sensor.Distance);
    }

    [Test]
    public override void CheckPoint()
    {
        Assert.AreEqual(Vector3.zero, _sensor.Point);
    }

    [Test]
    public override void CheckNormal()
    {
        Assert.That(_sensor.Normal, Is.EqualTo(Vector3.up).Using(Vector3ComparerWithEqualsOperator.Instance));
    }
}

internal class RaySensorWithOffset : SensorTestBase
{
    protected override Vector3 ShootPosition { get; set; } = Vector3.up;

    [SetUp]
    public void Setup()
    {
        _data = new SensorData(Vector3.zero, Vector3.down);
        Utils.CreatePrimitive(PrimitiveType.Plane);
        _sensor = new RaySensor(_data.Offset, _data.Direction);
        _isHit = _sensor.Shoot(ShootPosition);
    }

    [Test]
    public override void CheckDistance()
    {
        Assert.That(_sensor.Distance, Is.EqualTo(1f).Using(FloatEqualityComparer.Instance));
    }

    [Test]
    public override void CheckHit()
    {
        Assert.AreEqual(true, _isHit);
    }

    [Test]
    public override void CheckNormal()
    {
        Assert.That(_sensor.Normal, Is.EqualTo(Vector3.up).Using(Vector3ComparerWithEqualsOperator.Instance));
    }

    [Test]
    public override void CheckPoint()
    {
        Assert.AreEqual(Vector3.zero, _sensor.Point);
    }
}