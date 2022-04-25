using UnityEngine;
using Zenject;

public class GravitySystem
{
    [Inject] private PlayerGravityData _data;

    public Vector3 ApplyGravity()
    {
        return Physics.gravity * Time.deltaTime;
    }

    public float SetGroundedGravity()
    {
        return _data.GroundedGravity;
    }

}
