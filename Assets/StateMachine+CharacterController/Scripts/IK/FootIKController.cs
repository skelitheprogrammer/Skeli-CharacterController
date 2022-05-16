using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

public class FootIKController : MonoBehaviour
{
    [SerializeField] private TwoBoneIKConstraint _leftIK;
    [SerializeField] private TwoBoneIKConstraint _rightIK;

    [SerializeField] private SensorBehaviour _leftSensor;
    [SerializeField] private SensorBehaviour _rightSensor;

    [SerializeField] private OverrideTransform _overridePosition;

    [SerializeField] private float _footOffset;
    [SerializeField] private float _smoothTime;

    private TwoBoneIKConstraint[] _footIK;
    private Sensor[] _sensors;
    private Transform[] _footTargetTransforms;
    private Transform[] _footTipTransforms;
    private float[] _footDifferences;
    private readonly int[] _footHashes = new[] { Animator.StringToHash("LeftFeetIK"), Animator.StringToHash("RightFeetIK") };

    private float _positionDifference;
    private Vector3 _velocity;
    
    [Inject] private readonly Animator _animator;


    private void Start()
    {
        Time.timeScale = .5f;
        _footTargetTransforms = new[] { _leftIK.data.target, _rightIK.data.target };
        _footTipTransforms = new[] { _leftIK.data.tip, _rightIK.data.tip };
        _footIK = new[] { _leftIK, _rightIK };
        _sensors = new[] { _leftSensor.Sensor, _rightSensor.Sensor };
        _footDifferences = new float[2];
    }

    private void Update()
    {
        if (!_animator) return;

        for (int i = 0; i < 2; i++)
        {
            var footTipTransform = _footTipTransforms[i];
            var footTargetTransform = _footTargetTransforms[i];
            var footIK = _footIK[i];
            var sensor = _sensors[i];
            var feetWeight = _footHashes[i];

            var animWeight = _animator.GetFloat(feetWeight);

            footIK.transform.position = new Vector3(footTipTransform.position.x, footIK.transform.position.y, footTipTransform.position.z);
            
            if (sensor.IsHit)
            {
                var targetPosition = sensor.hit.point + Vector3.up * _footOffset;
                var difference = footTipTransform.position.y - targetPosition.y;
                footIK.weight = animWeight;

                footTargetTransform.position = targetPosition - Vector3.up * difference;
                footTargetTransform.rotation = Quaternion.FromToRotation(Vector3.up, sensor.hit.normal) * transform.rotation;
            }

        }

        //_positionDifference = Mathf.Abs(_footDifferences[0] + _footDifferences[1]);
        //_overridePosition.data.position = Vector3.SmoothDamp(_overridePosition.data.position, -Vector3.up * _positionDifference, ref _velocity, _smoothTime);
    }

}
