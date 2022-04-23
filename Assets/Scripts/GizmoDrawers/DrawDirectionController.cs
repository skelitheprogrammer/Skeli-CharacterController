using UnityEngine;
using Zenject;

public class DrawDirectionController : GizmosBase
{
    [SerializeField] private Vector3 _offset;

    [Min(1)]
    [SerializeField] private float _lengthMultiplier;

    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _lookSlopeColor;
    [SerializeField] private Color _slopeColor;
    [SerializeField] private Color _jumpColor;
    [SerializeField] private Color _cameraSlopeColor;

    [Inject] private CharacterStateData _data;

    protected override void DrawGizmo()
    {
        if (_data == null) return;

        var position = _data.transform.position + _offset;
        var normal = _data.normal;
        var slopeVector = _data.slopeVector;
        var lookSlopeVector = _data.lookSlopeVector;
        var jumpVector = _data.jumpVector;
        var cameraSlopeVector = _data.cameraSlopeVector;

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