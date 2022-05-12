using UnityEngine;
using Zenject;

public class PlayerRotationController : PlayerRotationControllerBase
{
    [Inject] private PlayerRotationSystem _rotation;

    public override Quaternion CalculatePlayerRotation()
    {
        return _rotation.CalculateRotationAngle();
    }
}
