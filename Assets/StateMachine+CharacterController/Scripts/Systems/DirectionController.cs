using UnityEngine;
using Zenject;

public class DirectionController
{
	[Inject] private readonly InputReader _input;
	[Inject(Id = IDConstants.DIRECTIONCHECK)] private readonly GroundCheckData _data;
	[Inject(Id = IDConstants.MAINCAMERA)] public Transform Camera { get; private set; }
	[Inject(Id = IDConstants.PLAYERTRANSFORM)] public Transform Player { get; private set; }
	[Inject(Id = IDConstants.GROUNDCHECK)] private readonly Sensor _sensor;

	public float Angle { get; private set; }
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

	public Vector3 GetCameraSlopeVector()
    {
		var forward = new Vector3(Camera.forward.x, 0, Camera.forward.z);
		var right = new Vector3(Camera.right.x, 0, Camera.right.z);
		var cameraDirection = (forward * _input.MoveInput.y + right * _input.MoveInput.x).normalized;

		CalculateNormal();
		return Vector3.ProjectOnPlane(cameraDirection, Normal).normalized;
	}

	public Vector3 GetJumpVector()
    {
		return (Vector3.up + GetCameraSlopeVector() * _input.MoveInputDirection.magnitude).normalized;
	}

    public float GetDot()
    {
		return Vector3.Dot(GetLookSlopeVector(), GetCameraSlopeVector());
    }

    private void CalculateNormal()
	{
		if (_sensor.hit.distance > _data.GroundDistance)
        {
			Normal = Vector3.zero;
			return;
        }

		Angle = Vector3.Angle(_sensor.hit.normal, Vector3.up);

		if (Angle > 1)
		{
			Normal = _sensor.hit.normal;
		}
		else
		{
			Normal = Vector3.up;
		}
	}
}