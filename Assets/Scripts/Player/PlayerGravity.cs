using UnityEngine;
using Zenject;

public class PlayerGravity
{
    [Inject] private GroundDetection _groundDetection;
    [Inject] private PlayerGameStatus _status;

    private float _groundedValue;

    public PlayerGravity(PlayerGravityData data)
    {
        _groundedValue = data.GroundedGravity;
    }

    public void ApplyGravity()
    {


    }
}
