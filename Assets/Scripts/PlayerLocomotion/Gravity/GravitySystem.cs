using UnityEngine;

public class GravitySystem
{
    private readonly PlayerGravityData _data;

    public float ApplyGravity()
    {
        return Physics.gravity.y * Time.deltaTime;
    }

    public float SetGroundedGravity()
    {
        return _data.GroundedGravity;
    }

}
