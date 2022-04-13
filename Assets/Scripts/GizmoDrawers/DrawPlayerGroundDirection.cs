using UnityEngine;
using Zenject;

public class DrawPlayerGroundDirection : MonoBehaviour
{
    [Inject] private DirectionController _direction;
    [Min(1)]
    [SerializeField] private float _lengthMultiplier = 2;

    [SerializeField] private Color _green;
    [SerializeField] private Color _lightGreen;
    [SerializeField] private Color _orange;

    private void OnDrawGizmos()
    {
        if (_direction == null) 
        {
            return;
        }

/*        Gizmos.color = _green;
        Gizmos.DrawRay(_direction.transform.position, _direction.LookSlopeVector * _lengthMultiplier);

        Gizmos.color = _lightGreen;
        Gizmos.DrawRay(_direction.transform.position, _direction.SlopeVector * _lengthMultiplier);

        Gizmos.color = _orange;
        Gizmos.DrawRay(_direction.transform.position, _direction.JumpVector * _lengthMultiplier);*/
    }
}