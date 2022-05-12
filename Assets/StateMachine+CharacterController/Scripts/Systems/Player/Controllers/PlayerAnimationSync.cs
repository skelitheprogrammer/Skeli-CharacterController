using UnityEngine;
using Zenject;

public class PlayerAnimationSync : MonoBehaviour
{
	[Inject] private readonly Animator _animator;
	[Inject] private readonly PlayerMovementData _moveData;

	[Range(0,1f)]
	[SerializeField] private float _blendSmoothTime;

	private float _velocity;
	
	private readonly int _isGroundedHash = Animator.StringToHash("Grounded");
	private readonly int _horizontalSpeedHash = Animator.StringToHash("HorizotnalSpeed");
	private readonly int _veritcalSpeedHash = Animator.StringToHash("VerticalSpeed");
	private readonly int _freefallHash = Animator.StringToHash("FreeFall");
	private readonly int _jumpTriggerHash = Animator.StringToHash("Jump");
	private readonly int _angleHash = Animator.StringToHash("Angle");
	private readonly int _velDotHash = Animator.StringToHash("VelDot");
	
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
	
	public void SetHorizontalSpeed(float currentValue)
	{
		//_animator.SetFloat(_horizontalSpeedHash, Mathf.SmoothDamp(0, currentValue / _moveData.MaxSpeed,ref _velocity, _blendSmoothTime));
		_animator.SetFloat(_horizontalSpeedHash, currentValue / _moveData.MaxSpeed);
	}

	public void SetVerticalSpeed(float currentValue)
    {
		_animator.SetFloat(_veritcalSpeedHash, currentValue);
	}
	
	public void SetAngle(float angle)
    {
		_animator.SetFloat(_angleHash, angle);
    }

	public void SetVelDot(float value)
    {
		_animator.SetFloat(_velDotHash, value);
    }
}
