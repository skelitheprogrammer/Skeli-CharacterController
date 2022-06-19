using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class LookRigController : MonoBehaviour
{
    [SerializeField] private MultiAimConstraint _bodyAim;
    [SerializeField] private MultiAimConstraint _headAim;
    [SerializeField] private Transform _lookTarget;
    [SerializeField] private float _distance;
    [SerializeField] private float _maxLookRadius;
    [SerializeField] private float _smoothTime;

    private readonly Transform _camera;
    private Transform _target;

    private float _velocity;

    private void Update()
    {
        if (_target != null)
        {
            LookAt(_target.position);
            return;
        }

        FollowCamera();
    } 

    public void FollowCamera()
    {
        var newPos = _camera.position + _camera.forward * _distance;
        LookAt(newPos);

        var other = (_lookTarget.position - transform.position).normalized;
        var angle = Vector3.Angle(transform.forward, other);

        if (angle > _maxLookRadius)
        {
            SetWeight(0);
        }
        else
        {
            SetWeight(1);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void SetWeight(float weight)
    {

        _bodyAim.weight = Mathf.SmoothDamp(_bodyAim.weight, weight, ref _velocity, _smoothTime);
        _headAim.weight = Mathf.SmoothDamp(_headAim.weight, weight, ref _velocity, _smoothTime);
    }

    public void LookAt(Vector3 pos)
    {
        _lookTarget.position = pos;
    }
}
