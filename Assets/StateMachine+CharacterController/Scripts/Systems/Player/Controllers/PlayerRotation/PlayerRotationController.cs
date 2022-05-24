using UnityEngine;
using Zenject;

public class PlayerRotationController
{
    private IRotationModule _currentRotationModule;
    private float _targetRotation;

    [Inject(Id = IDConstants.PLAYERTRANSFORM)] private readonly Transform _player;
    [Inject(Id = IDConstants.ROTATEORIGIN)] private readonly Transform _origin;

    private readonly FreeFormRotationModule _freeFormRotation;
    private readonly StrafeRotationModule _strafeRotation;
    private readonly InputReader _input;

    public PlayerRotationController(FreeFormRotationModule freeFormRotation, StrafeRotationModule strafeRotation, InputReader input)
    {
        _freeFormRotation = freeFormRotation;
        _strafeRotation = strafeRotation;
        _currentRotationModule = _freeFormRotation;
        _input = input;
    }

    public Quaternion CalculatePlayerRotation()
    {
        return _currentRotationModule.CalculateRotationAngle(_input.MoveInput, _player.eulerAngles, _origin.eulerAngles,ref _targetRotation);
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
