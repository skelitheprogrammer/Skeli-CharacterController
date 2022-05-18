using UnityEngine;
using Zenject;

public class PlayerMovementController : PlayerMovementControllerBase
{
    [Inject] private readonly FreeFormMovementModule _directionalMovement;

    public override Vector3 CalculateSpeed(Vector3 velocity)
    {
        return _directionalMovement.CalculateMovement(velocity);
    }
}
