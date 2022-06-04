using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

internal class RaySphereWithoutOffset : SensorTestBase
{
    protected override Vector3 ShootPosition { get; set; } = Vector3.up;
    private float _radius = .1f;

    [SetUp]
    public void Setup()
    {
        _data = new SensorData(Vector3.zero, Vector3.down);
        Utils.CreatePrimitive(PrimitiveType.Plane);
        _sensor = new RaySphereSensor(_data.Offset, _data.Direction, _radius);
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

internal class RaySphereWithOffset : SensorTestBase
{
    protected override Vector3 ShootPosition { get; set; } = Vector3.up;
    private float _radius = .1f;

    [SetUp]
    public void Setup()
    {
        _data = new SensorData(Vector3.up, Vector3.down);
        Utils.CreatePrimitive(PrimitiveType.Plane);
        _sensor = new RaySphereSensor(_data.Offset, _data.Direction, _radius);
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
        Assert.That(_sensor.Point, Is.EqualTo(Vector3.zero).Using(Vector3ComparerWithEqualsOperator.Instance));
    }
}
