public class LocomotionRotationController
{
    public readonly FreeFormRotationModule freeform;
    public readonly StrafeRotationModule strafe;

    public readonly RotationService service;

    public LocomotionRotationController(FreeFormRotationModule freeform, StrafeRotationModule strafe, RotationService service)
    {
        this.freeform = freeform;
        this.strafe = strafe;
        this.service = service;
    }
}