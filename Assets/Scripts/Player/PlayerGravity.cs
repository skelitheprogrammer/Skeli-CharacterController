using UnityEngine;

public class PlayerGravity
{
    private GroundDetection _groundDetection;

    private float _groundedValue;

    public PlayerGravity(GroundDetection groundDetection,PlayerGravityData data)
    {
        _groundDetection = groundDetection;
        _groundedValue = data.GroundedGravity;
    }

    public void ApplyGravity(ref Vector3 velocity)
    {
        if (_groundDetection.Detected)
        {
            velocity = new Vector3(velocity.x,_groundedValue,velocity.z);
        }
        else
        {
            var gravityForce = Physics.gravity.y * Time.deltaTime * Vector3.up;
            velocity += gravityForce;
        }
    }
}
