using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

public class BodyAimRigController : MonoBehaviour
{
    [SerializeField] private MultiAimConstraint _aimConstraint;
    [SerializeField] private float _distance;

    [Inject(Id = Constants.MAINCAMERA)] private Transform _camera;

     private Transform _target;

    private void Awake()
    {
        _target = _aimConstraint.data.sourceObjects[0].transform;
    }

    private void Update()
    {
        _target.position = _camera.position + _camera.forward * _distance;
    }
}
