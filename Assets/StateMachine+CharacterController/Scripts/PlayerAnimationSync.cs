using UnityEngine;
using Zenject;

public class PlayerAnimationSync : MonoBehaviour
{
	[Inject] private readonly Animator _animator;
	[Inject] private readonly PlayerMovementData _moveData;
	
	private readonly int _isGroundedHash = Animator.StringToHash("Grounded");
	private readonly int _speedHash = Animator.StringToHash("Speed");
	private readonly int _freefallHash = Animator.StringToHash("FreeFall");
	private readonly int _jumpTriggerHash = Animator.StringToHash("Jump");
	
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
	
	public void SetSpeed(float currentValue)
	{
		_animator.SetFloat(_speedHash, currentValue / _moveData.MaxSpeed);
	}
	
}
