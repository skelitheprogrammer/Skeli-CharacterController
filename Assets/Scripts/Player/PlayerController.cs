using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Inject] private CharacterController _controller;

    [Inject] private PlayerMovement _movement;
    [Inject] private PlayerRotation _rotation;
    [Inject] private PlayerGravity _gravity;
    [Inject] private PlayerJumping _jump;

    public Vector3 velocity;

    private void Update()
    {
        _movement.CharacterMove(ref velocity);
/*        _rotation.CharacterRotate(transform);

        _jump.Jump(ref velocity);
        _gravity.ApplyGravity(ref velocity);

        _controller.Move(velocity * Time.deltaTime);*/
    }
}
