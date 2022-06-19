using UnityEngine;

public class JumpController : ControllerBase<IJumpModule>
{
    public Vector3 MakeJump(object data)
    {
        return _module.CalculateJumpForce(data);
    }
}
