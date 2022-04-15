using UnityEngine;
using Zenject;

public class PlayerJumpController
{
    [Inject] private JumpCoyoteSettings _coyote;
    
    private float _jumpHeight;

    public PlayerJumpController(PlayerJumpingData data)
    {
        _jumpHeight = data.JumpHeight;
    }

    public void Procceed()
    {
        _coyote.Update();
    }

    public void Jump(Vector3 direction)
    {

    }
}
