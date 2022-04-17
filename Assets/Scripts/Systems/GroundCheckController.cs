using UnityEngine;
using Zenject;

public class GroundCheckController : ISystem, ITickable
{
    public bool Enabled { get; private set; } = true;
    [Inject(Id = Constants.PLAYERTRANSFORM)] public Transform transform;

    [Inject] private GroundCheckData _groundCheckData;
    [Inject] private CharacterStateData _stateData;

    public void Tick()
    {
        if (!Enabled) return;
        Procceed();
    }

    public void Procceed()
    {
        ref var normal = ref _stateData.normal;
        ref var angle = ref _stateData.slopeAngle;
        ref var isGrounded = ref _stateData.isGrounded;

        isGrounded = Physics.SphereCast(transform.position + _groundCheckData.Offset, _groundCheckData.Radius, Vector3.down,out var hit, _groundCheckData.Length);
        Debug.Log(isGrounded);
        angle = Vector3.Angle(hit.normal, Vector3.up);

        if (angle < 1)
        {
            normal = Vector3.up;
        }
        else
        {
            normal = hit.normal;
        }
    }

    public void Toggle()
    {
        Enabled = !Enabled;
    }

    public void Toggle(bool state)
    {
        Enabled = state;
    }


}
