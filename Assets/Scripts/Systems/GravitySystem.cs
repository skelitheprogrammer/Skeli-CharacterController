using UnityEngine;
using Zenject;

public class GravitySystem
{
    [Inject] private CharacterStateData _data;



    public void ApplyGravity()
    {

        if (_data.isGrounded)
        {
            _data.velocity.y = -.15f;
        }
        else
        {
            _data.velocity += Physics.gravity * Time.deltaTime;
        }
    }

}
