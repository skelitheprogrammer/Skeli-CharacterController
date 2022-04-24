using UnityEngine;
using Zenject;

public class GravitySystem
{
    public Vector3 ApplyGravity()
    {
        return Physics.gravity * Time.deltaTime;
    }

}
