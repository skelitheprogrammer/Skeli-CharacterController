using UnityEngine;
using Zenject;

public class DrawDirectionController : GizmosBase
{
    [Min(1)]
    [SerializeField] private float _lengthMultiplier;

    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _lookSlopeColor;
    [SerializeField] private Color _slopeColor;
    [SerializeField] private Color _jumpColor;

    [Inject] private CharacterStateData _characterStateData;

    protected override void DrawGizmo()
    {
        if (_characterStateData == null) return;

        var position = _characterStateData.Transform.position;
        var normal = _characterStateData.normal;
        var slopeVector = _characterStateData.slopeVector;
        var lookSlopeVector = _characterStateData.lookSlopeVector;
        var jumpVector = _characterStateData.jumpVector;

        Gizmos.color = _normalColor;
        Gizmos.DrawRay(position, normal * _lengthMultiplier);

        Gizmos.color = _lookSlopeColor;
        Gizmos.DrawRay(position, slopeVector * _lengthMultiplier);

        Gizmos.color = _slopeColor;
        Gizmos.DrawRay(position, lookSlopeVector * _lengthMultiplier);

        Gizmos.color = _jumpColor;
        Gizmos.DrawRay(position, jumpVector * _lengthMultiplier);

    }
}