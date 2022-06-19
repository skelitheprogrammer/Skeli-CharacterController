using UnityEngine;

public interface ISensorCaster
{
    float GetDistance();
    Vector3 GetPoint();
    Vector3 GetNormal();

    bool Shoot(Vector3 position);
}
