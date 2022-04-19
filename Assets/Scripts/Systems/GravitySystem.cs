using UnityEngine;
using Zenject;

public class GravitySystem
{
    [Inject] CharacterStateData _data;

    public Vector3 ApplyGravity(Vector3 velocity)
    {
        if (_data.isGrounded)
        {
            velocity = new Vector3(velocity.x, -.15f, velocity.z);
        }
        else
        {
            velocity += Physics.gravity * Time.deltaTime;
        }
       
        return velocity;
    }
}
