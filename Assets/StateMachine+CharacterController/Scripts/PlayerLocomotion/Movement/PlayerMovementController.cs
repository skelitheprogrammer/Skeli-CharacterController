using UnityEngine;

public class PlayerMovementController
{
    private readonly FreeFormMovementModule _freeFormModule;
    private readonly StrafeMovementModule _strafeModule;
    private readonly AirControlModule _airControlModule;

    private IMovementModule _movementModule;

    public PlayerMovementController(FreeFormMovementModule directionalMovement, StrafeMovementModule strafeMovement, AirControlModule airControlModule)
    {
        _freeFormModule = directionalMovement;
        _strafeModule = strafeMovement;
        _movementModule = directionalMovement;
        _airControlModule = airControlModule;
    }

    public Vector3 CalculateSpeed(Vector3 velocity)
    {
        return _movementModule.CalculateMovement(velocity);
    }

    public void SetAirControl()
    {
        _movementModule = _airControlModule;
    }

    public void SetFreeForm()
    {
        _movementModule = _freeFormModule;
    }

    public void SetStrafe()
    {
        _movementModule = _strafeModule;
    }

}
