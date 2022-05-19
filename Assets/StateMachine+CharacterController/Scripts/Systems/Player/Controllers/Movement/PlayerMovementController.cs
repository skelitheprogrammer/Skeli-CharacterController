using UnityEngine;
using Zenject;

public class PlayerMovementController : PlayerMovementControllerBase
{
    private readonly FreeFormMovementModule _directionalMovement;
    private readonly StrafeMovementModule _strafeMovement;

    private IMovementModule _movementModule;

    public PlayerMovementController(FreeFormMovementModule directionalMovement, StrafeMovementModule strafeMovement)
    {
        _directionalMovement = directionalMovement;
        _strafeMovement = strafeMovement;
        _movementModule = _directionalMovement;
    }

    public override Vector3 CalculateSpeed(Vector3 velocity)
    {
        return _movementModule.CalculateMovement(velocity);
    }

    public void ToggleMovement()
    {
        if (_movementModule == _directionalMovement)
        {
            _movementModule = _strafeMovement;
        }
        else
        {
            _movementModule = _directionalMovement;
        }
    }
}
