using UnityEngine;
using Zenject;

public class PlayerGravity
{
    [Inject] private PlayerGroundDetection _groundDetection;
    [Inject] private InputReader _input;
    [Inject] private CharacterController _controller;

    [SerializeField] private float _groundedValue;
    private Vector3 _currentGravity;

    public void ApplyGravity()
    {
        if (_groundDetection.Detected && !_input.IsJumped)
        {
            _currentGravity = Vector3.up * _groundedValue;
        }
        else
        {
            var gravityForce = Physics.gravity.y * Time.deltaTime * Vector3.up;
            AddForce(gravityForce);
        }

        _controller.Move(_currentGravity * Time.deltaTime);
    }

    public void AddForce(Vector3 force)
    {
        _currentGravity += force;
    }
}
