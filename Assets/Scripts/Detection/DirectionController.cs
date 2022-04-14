using UnityEngine;
using Zenject;

public class DirectionController : MonoBehaviour
{
    [Inject] private GroundDetection _detection;
    [Inject] private InputReader _input;

    public Vector3 LookSlopeVector { get; private set; }
    public Vector3 SlopeVector { get; private set; }
    public Vector3 JumpVector { get; private set; }

    public void Update()
    {
        TryDetect();
    }

    public void TryDetect()
    {
        var normal = _detection.GroundNormal;
        var inputDirection = _input.MoveInputDirection;

        LookSlopeVector = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
        SlopeVector = Vector3.ProjectOnPlane(Vector3.down, normal).normalized;
        JumpVector = (Vector3.up + (LookSlopeVector * inputDirection.magnitude)).normalized;
    }
}