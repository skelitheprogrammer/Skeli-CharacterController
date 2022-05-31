using UnityEngine;
using Zenject;

public class DirectionController
{
    [Inject] private readonly InputReader _input;
    [Inject(Id = IDConstants.DIRECTIONCHECK)] private readonly GroundCheckData _data;
    [Inject(Id = IDConstants.MAINCAMERA)] public Transform Camera { get; private set; }
    [Inject(Id = IDConstants.PLAYERTRANSFORM)] public Transform Player { get; private set; }
    [Inject(Id = IDConstants.GROUNDCHECK)] private readonly Sensor _groundSensor;
    [Inject(Id = IDConstants.DIRECTIONCHECK)] private readonly Sensor _directionSensor;

    public float Angle { get; private set; }
    public bool IsOnSlope => Angle > 1;

    public Vector3 Normal { get; private set; }

    public Vector3 GetSlopeVector()
    {
        CalculateNormal();
        return Vector3.ProjectOnPlane(Vector3.down, Normal);
    }

    public Vector3 GetLookSlopeVector()
    {
        CalculateNormal();
        return Vector3.ProjectOnPlane(Player.forward, Normal);
    }

    public Vector3 GetPlayerForwardInput()
    {
		return Player.forward * _input.MoveInput.y + Player.right * _input.MoveInput.x;
    }

    public Vector3 GetCameraVector()
    {
        var forward = new Vector3(Camera.forward.x, 0, Camera.forward.z);
        var right = new Vector3(Camera.right.x, 0, Camera.right.z);
        var cameraDirection = forward * _input.MoveInput.y + right * _input.MoveInput.x;

        return cameraDirection.normalized;
    }

    public Vector3 GetCameraSlopeVector()
    {
        var cameraDirection = GetCameraVector();

        CalculateNormal();
        return Vector3.ProjectOnPlane(cameraDirection, Normal).normalized;
    }

    public Vector3 GetJumpVector()
    {
        return (Vector3.up + GetCameraSlopeVector()).normalized;
    }

    public float GetDot()
    {
        return Vector3.Dot(GetLookSlopeVector(), GetCameraSlopeVector());
    }

    private void CalculateNormal()
    {
        if (_groundSensor.Distance > _data.GroundDistance)
        {
            Normal = Vector3.zero;
            return;
        }

        if (_directionSensor.Distance > _groundSensor.Distance)
        {
            Angle = Vector3.Angle(_directionSensor.Normal, Vector3.up);

            if (IsOnSlope)
            {
                Normal = _directionSensor.Normal;
            }
            else
            {
                Normal = Vector3.up;
            }
        }
        else
        {
            Angle = Vector3.Angle(_groundSensor.Normal, Vector3.up);

            if (IsOnSlope)
            {
                Normal = _groundSensor.Normal;
            }
            else
            {
                Normal = Vector3.up;
            }
        }


    }
}