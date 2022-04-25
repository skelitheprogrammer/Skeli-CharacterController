using UnityEngine;
using Zenject;

[System.Serializable]
public class CharacterStateData
{
    [Inject(Id = Constants.PLAYERTRANSFORM)] public Transform playerTransform;
    [Inject(Id = Constants.MAINCAMERA)] public Transform camera;
    [Inject(Id = Constants.ROTATEORIGIN)] public Transform rotateOrigin;
    public bool isGrounded;
    public bool canJump;
    public Vector3 playerDirection;
    public Vector3 lastDirection;
    public Vector3 normal;
    public Vector3 slopeVector;
    public Vector3 lookSlopeVector;
    public Vector3 cameraSlopeVector;
    public Vector3 jumpVector;
    public Vector3 velocity;
    public Vector3 neededAccel;
    public float slopeAngle;
}