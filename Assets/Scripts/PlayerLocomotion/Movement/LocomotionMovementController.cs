using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionMovementController
{
    public readonly AirControlModule airControl;
    public readonly FreeFormMovementModule freeform;
    public readonly StrafeMovementModule strafe;

    public readonly MovementService service;

    public LocomotionMovementController(AirControlModule airControl, FreeFormMovementModule freeform, StrafeMovementModule strafe, MovementService service)
    {
        this.airControl = airControl;
        this.freeform = freeform;
        this.strafe = strafe;
        this.service = service;
    }
}
