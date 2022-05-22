using UnityEngine;

public class CameraZoomModule
{

    public float CalculateZoomDelta(float amount)
    {
        return amount * Time.deltaTime;
    }
}