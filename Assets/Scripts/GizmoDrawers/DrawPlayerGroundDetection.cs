using UnityEngine;
using Zenject;

public class DrawPlayerGroundDetection : MonoBehaviour
{
    private GroundDetection _detection;

    [SerializeField] private bool _enable = true;

    [SerializeField] private Color _detectedColor;
    [SerializeField] private Color _unDetectedColor;

    private void OnDrawGizmos()
    {
        if (!_enable) return;
        if (_detection == null)
        {
            TryGetComponent(out _detection);
            return; 
        }

        Gizmos.color = _unDetectedColor;

        if (_detection.IsDetected)
        {
            Gizmos.color = _detectedColor;
        }

        var totalOffset = _detection.transform.position + _detection.Offset;
        var totalDistance = Vector3.down * _detection.Distance;

        Gizmos.DrawRay(totalOffset, totalDistance);
        Gizmos.DrawWireSphere(totalOffset + totalDistance, _detection.Radius);
    }
}
