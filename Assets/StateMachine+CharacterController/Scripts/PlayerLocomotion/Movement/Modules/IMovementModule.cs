using UnityEngine;

public interface IMovementModule
{
    Vector3 CalculateMovement(in Vector3 velocity);
}
