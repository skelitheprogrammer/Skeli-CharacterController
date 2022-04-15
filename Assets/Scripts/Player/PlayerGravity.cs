using UnityEngine;
using Zenject;

public class PlayerGravity
{
    [Inject] private GroundDetection _groundDetection;
    [Inject] private PlayerGameStatus _status;

    private float _groundedValue;
    private Vector3 _currentGravity;

    public PlayerGravity(PlayerGravityData data)
    {
        _groundedValue = data.GroundedGravity;
    }

    public void ApplyGravity()
    {
        if (_groundDetection.IsDetected)
        {
            _currentGravity = Vector3.up * _groundedValue;
        }
        else
        {
            var gravityForce = Physics.gravity.y * Time.deltaTime * Vector3.up;
            _currentGravity += gravityForce;
        }

        _status.velocity += _currentGravity;

    }

    public void AddForce(Vector3 force)
    {
        _currentGravity += force;
    }
}
