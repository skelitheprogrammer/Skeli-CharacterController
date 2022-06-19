using UnityEngine;

public class OriginRotationModule
{
	private readonly float _topClamp;
	private readonly float _botClamp;
	private readonly float _rotationFactor;

	private float _yaw;
	private float _pitch;

    private const float _threshold = 0.01f;

    public OriginRotationModule(float topClamp, float botClamp, float rotationFactor)
    {
        _topClamp = topClamp;
        _botClamp = botClamp;
        _rotationFactor = rotationFactor;
    }

    public Quaternion RotateOrigin(Vector2 rotateInput)
	{

		if (rotateInput.sqrMagnitude >= _threshold)
		{
			_yaw += rotateInput.x * _rotationFactor * Time.unscaledDeltaTime;
			_pitch += rotateInput.y * _rotationFactor * Time.unscaledDeltaTime;
		}

		_yaw = Utils.ClampAngle(_yaw, float.MinValue, float.MaxValue);
		_pitch = Utils.ClampAngle(_pitch, _botClamp, _topClamp);

		return Quaternion.Euler(-_pitch, _yaw, 0.0f);
	}

}
