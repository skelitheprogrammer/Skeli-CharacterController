using UnityEngine;
using Zenject;

public class PlayerRotationController : PlayerRotationControllerBase
{
    [Inject] private readonly PlayerFreeFormRotationSystem _freeFormRotation;
    [Inject] private readonly PlayerStrafeRotationSystem _strafeRotation;

    [Inject] private IPlayerRotationSystem _currentRotationSystem;

    public override Quaternion CalculatePlayerRotation()
    {
        return _currentRotationSystem.CalculateRotationAngle();
    }

    public void Toggle()
    {

        if (_currentRotationSystem == _freeFormRotation)
        {
            _currentRotationSystem = _strafeRotation;
        }
        else
        {
            _currentRotationSystem = _freeFormRotation;
        }
    }
}
