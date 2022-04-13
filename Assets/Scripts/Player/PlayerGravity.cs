using UnityEngine;

public class PlayerGravity
{
    private GroundDetection _groundDetection;

    private float _groundedValue;

    public PlayerGravity(GroundDetection groundDetection, float groundedValue)
    {
        _groundDetection = groundDetection;
        _groundedValue = groundedValue;
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
