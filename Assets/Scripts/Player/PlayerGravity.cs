using UnityEngine;
using Zenject;

public class PlayerGravity
{
    [Inject] private PlayerGameStatus _status;

    private float _groundedValue;
    public Vector3 _currentGravity;

    public PlayerGravity(PlayerGravityData data)
    {
        _groundedValue = data.GroundedGravity;
    }

    public void ApplyGravity()
    {
        _currentGravity.y += Physics.gravity.y * Time.deltaTime;
    }

    public void ResetGravity()
    {
        _currentGravity.y = _groundedValue;
    }
}
