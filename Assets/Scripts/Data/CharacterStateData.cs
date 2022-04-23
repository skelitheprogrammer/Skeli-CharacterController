using UnityEngine;
using Zenject;

[System.Serializable]
public class CharacterStateData
{
    [Inject(Id = Constants.PLAYERTRANSFORM)] public Transform transform;
    public bool isGrounded;
    public bool canJump;
    public Vector3 normal;
    public Vector3 slopeVector;
    public Vector3 lookSlopeVector;
    public Vector3 cameraSlopeVector;
    public Vector3 jumpVector;
    public Vector3 velocity;
    public float slopeAngle;
}