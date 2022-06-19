using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	private float _horizontalVelocity;
	private float _verticalVelocity;
	private float _currentVerticalSpeed;
	
	private readonly Animator _animator;
	private readonly PlayerMovementData _moveData;

	[SerializeField] private float _blendSmoothTime;
	
	private readonly int _isGroundedHash = Animator.StringToHash("Grounded");
	private readonly int _horizontalSpeedHash = Animator.StringToHash("HorizotnalSpeed");
	private readonly int _veritcalSpeedHash = Animator.StringToHash("VerticalSpeed");
	private readonly int _speed = Animator.StringToHash("Speed");
	private readonly int _freefallHash = Animator.StringToHash("FreeFall");
	private readonly int _jumpTriggerHash = Animator.StringToHash("Jump");
	private readonly int _angleHash = Animator.StringToHash("Angle");
	private readonly int _velDotHash = Animator.StringToHash("VelDot");
	private readonly int _isStrafingHash = Animator.StringToHash("IsStrafing");
	private readonly int _gravity = Animator.StringToHash("Gravity");
	private readonly int _isMoving = Animator.StringToHash("IsMoving");
	
	public void TriggerJump()
	{
		_animator.SetTrigger(_jumpTriggerHash);	
	}
	
	public void ResetTriggerJump()
    {
		_animator.ResetTrigger(_jumpTriggerHash);
    }

	public void SetFreeFall(bool state)
	{
		_animator.SetBool(_freefallHash, state);
	}
	
	public void SetIsGrounded(bool state)
	{
		_animator.SetBool(_isGroundedHash, state);
	}
	
	public void SetHorizontalSpeed(float value)
	{
		SetIsMoving(value);

		var currentValue = _animator.GetFloat(_horizontalSpeedHash);
		var newValue = Mathf.MoveTowards(currentValue, value, _blendSmoothTime * Time.deltaTime);
		_animator.SetFloat(_horizontalSpeedHash, newValue);
	}

	public void SetVerticalSpeed(float value)
    {
		SetIsMoving(value);

		var currentValue = _animator.GetFloat(_veritcalSpeedHash);
		var newValue = Mathf.MoveTowards(currentValue, value, _blendSmoothTime * Time.deltaTime);
		_animator.SetFloat(_veritcalSpeedHash, newValue);
	}
	
	public void SetSpeed(float value)
    {
		SetIsMoving(value);

		value = Mathf.Abs(value);

		var currentValue = _animator.GetFloat(_speed);
		var newValue = Mathf.MoveTowards(currentValue, value, _blendSmoothTime * Time.deltaTime);
		_animator.SetFloat(_speed, newValue);
	}
	
	public void SetAngle(float angle)
    {
		_animator.SetFloat(_angleHash, angle);
    }

	public void SetVelDot(float value)
    {
		_animator.SetFloat(_velDotHash, value);
    }

	public void SetIsStrafing(bool state)
    {
		_animator.SetBool(_isStrafingHash, state);
    }

	public void SetGravity(float value)
    {
		_animator.SetFloat(_gravity, value);
    }

	private void SetIsMoving(float value)
    {
		if (value == 0)
		{
			_animator.SetBool(_isMoving, false);
			return;
		}

		_animator.SetBool(_isMoving, true);


	}
}
