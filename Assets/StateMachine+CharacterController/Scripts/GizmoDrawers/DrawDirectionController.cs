using UnityEngine;
using Zenject;
#if UNITY_EDITOR
public class DrawDirectionController : DrawGizmosBase
{
    [SerializeField] private Vector3 _offset;

    [Min(1)]
    [SerializeField] private float _lengthMultiplier;

    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _lookSlopeColor;
    [SerializeField] private Color _slopeColor;
    [SerializeField] private Color _jumpColor;
    [SerializeField] private Color _cameraSlopeColor;

    [Inject] private readonly DirectionController _data = null;

    protected override void DrawGizmo()
    {
        if (_data == null) return;

        var position = _data.Player.position + _offset;
        var normal = _data.Normal;
        var slopeVector = _data.GetSlopeVector();
        var lookSlopeVector = _data.GetLookSlopeVector();
        var jumpVector = _data.GetJumpVector();
        var cameraSlopeVector = _data.GetCameraSlopeVector();

        Gizmos.color = _normalColor;
        Gizmos.DrawRay(position, normal * _lengthMultiplier);

        Gizmos.color = _lookSlopeColor;
        Gizmos.DrawRay(position, lookSlopeVector * _lengthMultiplier);

        Gizmos.color = _slopeColor;
        Gizmos.DrawRay(position, slopeVector * _lengthMultiplier);

        Gizmos.color = _jumpColor;
        Gizmos.DrawRay(position, jumpVector * _lengthMultiplier);

        Gizmos.color = _cameraSlopeColor;
        Gizmos.DrawRay(position, cameraSlopeVector * _lengthMultiplier);


    }
}
#endif