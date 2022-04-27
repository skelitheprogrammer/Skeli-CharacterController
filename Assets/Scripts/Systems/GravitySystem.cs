using UnityEngine;
using Zenject;

public class GravitySystem : GameSystem
{
    [Inject] private PlayerGravityData _data;

    public Vector3 ApplyGravity()
    {
        if (!_enabled) return Vector3.zero;

        return Physics.gravity * Time.deltaTime;
    }

    public float SetGroundedGravity()
    {
        if (!_enabled) return 0;

        return _data.GroundedGravity;
    }

}
