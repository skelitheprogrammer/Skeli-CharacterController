using UnityEngine;
using Zenject;

public class OriginRotationSystem : GameSystem, IInitializable
{
	[Inject] private InputReader _input;
	[Inject] private OriginRotationData _data;
	[Inject(Id = Constants.ROTATEORIGIN)] private Transform _rotationOrigin;

	private float _yaw;
	private float _pitch;

    private const float _threshold = 0.01f;
    
    public void Initialize()
    {
        _pitch = _rotationOrigin.eulerAngles.x;
    }

    public override void DoLogic()
    {
        OriginRotation();
    }

	private void OriginRotation()
	{
		var topClamp = _data.TopClamp;
		var botClamp = _data.BotClamp;

		if (_input.RotateInput.sqrMagnitude >= _threshold)
		{
			_yaw += _input.RotateInput.x * _data.Sensitivity * Time.deltaTime;
			_pitch += _input.RotateInput.y * _data.Sensitivity * Time.deltaTime;
		}

		_yaw = ClampAngle(_yaw, float.MinValue, float.MaxValue);
		_pitch = ClampAngle(_pitch, botClamp, topClamp);

		_rotationOrigin.transform.rotation = Quaternion.Euler(-_pitch, _yaw, 0.0f);
	}

	private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
	{
		if (lfAngle < -360f) lfAngle += 360f;
		if (lfAngle > 360f) lfAngle -= 360f;
		return Mathf.Clamp(lfAngle, lfMin, lfMax);
	}


}
