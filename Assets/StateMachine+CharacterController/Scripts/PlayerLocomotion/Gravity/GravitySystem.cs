using UnityEngine;
using Zenject;

public class GravitySystem
{
    [Inject] private readonly PlayerGravityData _data;

    public float ApplyGravity()
    {
        return Physics.gravity.y * Time.deltaTime;
    }

    public float SetGroundedGravity()
    {
        return _data.GroundedGravity;
    }

}
