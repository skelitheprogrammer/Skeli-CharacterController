using UnityEngine;

public sealed class MovementService : ControllerBase<IMovementModule>
{
    public Vector3 CalculateSpeed()
    {
        return _module.CalculateMovement();
    }

}
