using UnityEngine;
using Zenject;

public class DrawPlayerGroundDetection : MonoBehaviour
{
    private PlayerGroundDetection _detection;
    [SerializeField] private bool _enable = true;

    private void OnDrawGizmos()
    {
        if (!_enable) return;
        if (_detection == null)
        {
            TryGetComponent(out _detection);
        }

        Gizmos.color = Color.red;

        if (_detection.Detected)
        {
            Gizmos.color = Color.green;
        }

        var totalOffset = _detection.transform.position + _detection.Offset;
        var totalDistance = Vector3.down * _detection.Distance;

        Gizmos.DrawRay(totalOffset, totalDistance);
        Gizmos.DrawWireSphere(totalOffset + totalDistance, _detection.Radius);
    }
}
