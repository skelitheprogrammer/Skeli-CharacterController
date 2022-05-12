using UnityEngine;
using Zenject;

public class GroundCheckController : GameSystem
{
    [Inject] private GroundCheckData _groundCheckData;
    [Inject(Id = Constants.PLAYERTRANSFORM)] private Transform _playerTransform;

    public RaycastHit hit;

    public bool GroundCheck()
    {
        if (!_enabled) return false;

        var pos = _playerTransform.position + _groundCheckData.RayOffset;
        Physics.Raycast(pos, Vector3.down, out hit);

        if (hit.distance <= _groundCheckData.GroundDistance) return true;

        return false;
    }
}
