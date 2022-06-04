using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

public class FootIKController : MonoBehaviour
{
    [SerializeField] private MultiParentConstraint _leftRef;
    [SerializeField] private MultiParentConstraint _rightRef;

    [SerializeField] private TwoBoneIKConstraint _leftIK;
    [SerializeField] private TwoBoneIKConstraint _rightIK;

    [SerializeField] private Transform _leftTarget;
    [SerializeField] private Transform _rightTarget;

    [SerializeField] private OverrideTransform _overridePosition;

    [SerializeField] private float _footOffset;

    private ISensorCaster _leftSensor;
    private ISensorCaster _rightSensor;

    private MultiParentConstraint[] _footRefs;
    private TwoBoneIKConstraint[] _footIK;
    private ISensorCaster[] _sensors;
    private Transform[] _footTargetTransforms;
    private readonly int[] _footHashes = new[] { Animator.StringToHash("LeftFeetIK"), Animator.StringToHash("RightFeetIK") };
 
    [Inject] private readonly Animator _animator;

    private void Awake()
    {
        _leftSensor = new RaySensor(Vector3.up, Vector3.down);
        _rightSensor = new RaySensor(Vector3.up, Vector3.down);
        _footRefs = new[] { _leftRef, _rightRef };
        _footTargetTransforms = new[] { _leftTarget, _rightTarget };
        _footIK = new[] { _leftIK, _rightIK };
        _sensors = new[] { _leftSensor, _rightSensor};
    }

    private void LateUpdate()
    {
        for (int i = 0; i < 2; i++)
        {
            var feetRef = _footRefs[i];
            var feetTargetTransform = _footTargetTransforms[i];
            var feetIK = _footIK[i];
            var feetWeight = _footHashes[i];
            var sensor = _sensors[i];

            var animWeight = _animator.GetFloat(feetWeight);

            if (sensor.Shoot(feetRef.transform.position))
            {
                feetIK.weight = animWeight;
                feetTargetTransform.position = sensor.Point + Vector3.up * _footOffset;
                feetTargetTransform.rotation = Quaternion.FromToRotation(Vector3.up, sensor.Normal) * transform.rotation;
            }

        }        

    }

}
