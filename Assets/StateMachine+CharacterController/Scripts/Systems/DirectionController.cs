using UnityEngine;
using Zenject;

public class DirectionController
{
	[Inject] private readonly GroundCheckData _groundCheckData;
	[Inject] private readonly InputReader _input;

	[Inject(Id = Constants.MAINCAMERA)] public Transform Camera { get; private set; }
	[Inject(Id = Constants.PLAYERTRANSFORM)] public Transform Player { get; private set; }

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
		Physics.Raycast(Player.position + _groundCheckData.RayOffset, Vector3.down, out var hit, _groundCheckData.Length);

		Angle = Vector3.Angle(hit.normal, Vector3.up);

		if (Angle > 1)
		{
			Normal = hit.normal;
		}
		else
		{
			Normal = Vector3.up;
		}
	}
}