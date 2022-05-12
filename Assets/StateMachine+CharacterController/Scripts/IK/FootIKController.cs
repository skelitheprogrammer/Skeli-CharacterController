using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

public class FootIKController : MonoBehaviour
{
    [field: SerializeField] public TwoBoneIKConstraint _leftIK;
    [field: SerializeField] public TwoBoneIKConstraint _rightIK;
    [SerializeField] private OverrideTransform _overridePosition;

    [SerializeField] private float _smoothTime;
    [Range(0,1f)]
    [SerializeField] private float _weightTreshold;
    [Range(0,1f)]
    [SerializeField] private float _weightSmoothFactor = .5f;

    [SerializeField] private float _check;

    [SerializeField] private FootIKDataSO _data;

    [SerializeField] private Animator _animator;

    private readonly int _leftFeetHash = Animator.StringToHash("LeftFeetIK");
    private readonly int _rightFeetHash = Animator.StringToHash("RightFeetIK");

    private TwoBoneIKConstraint[] _footIK;
    private Transform[] _footTargetTransforms;
    private Transform[] _footTransforms;
    public Transform[] FootTipTransforms { get; private set; }
    public Vector3[] FootHitPoint { get; private set; }

    private float _positionDifference;
    private Vector3 _velocity;

    private void Start()
    {
        Time.timeScale = .4f;
        _footTargetTransforms = new[] { _leftIK.data.target, _rightIK.data.target };
        _footTransforms = new[] { _leftIK.data.mid, _rightIK.data.mid };
        FootTipTransforms = new[] { _leftIK.data.tip, _rightIK.data.tip };
        _footIK = new[] { _leftIK, _rightIK };
        FootHitPoint = new Vector3[2];
    }

    private void Update()
    {
        if (!_animator) return;

        for (int i = 0; i < 2; i++)
        {
            var footTipTransform = FootTipTransforms[i];
            var footTargetTransform = _footTargetTransforms[i];
            var footIK = _footIK[i];

            var pos = footTipTransform.position + _data.Data.RayOffset;
            
            if (Physics.SphereCast(pos, _data.Data.Radius, Vector3.down, out var hit))
            {
                FootHitPoint[i] = hit.point;

                var difference = footTipTransform.position.y - hit.point.y;
                var differencePerc = difference / hit.distance;

                var newWeight = 1 - differencePerc;

                footIK.weight = newWeight;


                footTargetTransform.position = footTipTransform.position - Vector3.up * difference + footTipTransform.up * footIK.transform.localPosition.y;
                footTargetTransform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation;
            }
        }

/*        _overridePosition.data.position = Vector3.SmoothDamp(_overridePosition.data.position, -Vector3.up * _positionDifference, ref _velocity, _smoothTime);*/
    }

}
