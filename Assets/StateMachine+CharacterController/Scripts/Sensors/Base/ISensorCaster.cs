using UnityEngine;

public interface ISensorCaster
{
    float Distance { get; }
    Vector3 Point { get; }
    Vector3 Normal { get; }

    bool Shoot(Vector3 position);
}
