using UnityEngine;
using Zenject;

public class CharacterStateData
{
    [Inject(Id = Constants.PLAYERTRANSFORM)] public readonly Transform Transform;
    public bool isGrounded;
    public Vector3 normal;
    public Vector3 slopeVector;
    public Vector3 lookSlopeVector;
    public Vector3 jumpVector;
    public float slopeAngle;
}