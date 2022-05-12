using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerJumpControllerBase
{
    public abstract bool CanJump { get; }

    public abstract void CalculateCanJump();
    public abstract Vector3 CalculateJumpForce();
}
