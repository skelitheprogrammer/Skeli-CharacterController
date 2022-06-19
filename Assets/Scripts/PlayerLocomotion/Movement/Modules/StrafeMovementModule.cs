using UnityEngine;

public class StrafeMovementModule : IMovementModule
{
    private StrafeMovementData _data;

    public StrafeMovementModule(StrafeMovementData data)
    {
        _data = data;
    }

    public Vector3 CalculateMovement()
    {
        var forwardAmount = Vector3.Dot(_data.lookDirection, _data.forwardInput);

        var speed = forwardAmount switch
        {
            < -.5f => _data.backwardsSpeed,
            < .5f => _data.sideWaysSpeed,
            _ => _data.forwardSpeed,
        };

        var targetVelocity = _data.direction * speed;

        return targetVelocity;
    }

}

public class StrafeMovementData
{
    public Vector3 direction;
    public Vector3 lookDirection;
    public Vector3 forwardInput;
    public float backwardsSpeed;
    public float sideWaysSpeed;
    public float forwardSpeed;
}