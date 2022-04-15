using UnityEngine;
using Zenject;

public class DrawCanJump : MonoBehaviour
{
    [Inject] private PlayerGameStatus _status;

    [SerializeField] private bool _enabled = true;

    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _radius;

    [SerializeField] private Color _canJumpColor;
    [SerializeField] private Color _cantJumpColor;

    private void OnDrawGizmos()
    {
        if (!_enabled) return;
        if (_status == null) return;

        if (_status.canJump)
        {
            Gizmos.color = _canJumpColor;
        }
        else
        {
            Gizmos.color = _cantJumpColor;
        }

        var pos = transform.position + _offset;

        Gizmos.DrawSphere(pos, _radius);
    }
}