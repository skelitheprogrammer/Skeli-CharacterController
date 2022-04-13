using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerJumping _jump;
    [SerializeField] private PlayerGravity _gravity;
    [SerializeField] private PlayerRotation _rotation;

    public Vector3 velocity;

    private void Update()
    {
        _movement.CharacterMove();
        _rotation.CharacterRotate();

        _jump.TryJump();
        _gravity.ApplyGravity();

    }
}
