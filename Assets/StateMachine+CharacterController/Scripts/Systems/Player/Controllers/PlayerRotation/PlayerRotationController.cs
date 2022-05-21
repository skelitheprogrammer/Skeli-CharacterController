using UnityEngine;

public class PlayerRotationController : PlayerRotationControllerBase
{
    private readonly FreeFormRotationModule _freeFormRotation;
    private readonly StrafeRotationModule _strafeRotation;

    private IRotationModule _currentRotationModule;

    public PlayerRotationController(FreeFormRotationModule freeFormRotation, StrafeRotationModule strafeRotation)
    {
        _freeFormRotation = freeFormRotation;
        _strafeRotation = strafeRotation;
        _currentRotationModule = _freeFormRotation;
    }

    public override Quaternion CalculatePlayerRotation()
    {
        return _currentRotationModule.CalculateRotationAngle();
    }

    private void SetRotation(IRotationModule system)
    {
        _currentRotationModule = system;
    }

    public void ToggleRotation()
    {
        if (_currentRotationModule == _freeFormRotation)
        {
            SetRotation(_strafeRotation);
        }
        else
        {
            SetRotation(_freeFormRotation);
        }
    }

}
