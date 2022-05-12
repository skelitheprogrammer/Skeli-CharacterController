using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerCameraController : CameraControllerBase
{
    [Inject] private readonly OriginRotationSystem _originRotation;

    public override void ControlCamera()
    {
        _originRotation.RotateOrigin();
    }
}
