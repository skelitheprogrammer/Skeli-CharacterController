using UnityEngine;
using Zenject;

[System.Serializable]
public class CharacterStateData
{
    [Inject(Id = Constants.PLAYERTRANSFORM)] public Transform Transform;
    public bool isGrounded;
    public Vector3 normal;
    public Vector3 slopeVector;
    public Vector3 lookSlopeVector;
    public Vector3 jumpVector;
    public float slopeAngle;
}