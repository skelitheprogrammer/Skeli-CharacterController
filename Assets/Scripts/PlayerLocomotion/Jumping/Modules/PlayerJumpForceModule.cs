using UnityEngine;

public class PlayerJumpForceModule : IJumpModule
{ 
    public Vector3 CalculateJumpForce(object incomeData)
    {
        var data = incomeData as PlayerJumpForceData;

        var jumpHeight = Mathf.Sqrt(-2 * Physics.gravity.y * data.jumpForce);

        return data.direction * jumpHeight;
    }
}

public class PlayerJumpForceData
{
    public Vector3 direction;
    public float jumpForce;

    public PlayerJumpForceData(Vector3 direction, float jumpForce)
    {
        this.direction = direction;
        this.jumpForce = jumpForce;
    }
}