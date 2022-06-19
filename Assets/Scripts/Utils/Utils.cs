using UnityEngine;

public static class Utils
{
    public static bool CheckDistance(float distance, float comparableDistance)
    {
        return distance <= comparableDistance;
    }

    public static Vector3 GetInputDirection(Vector3 forward, Vector3 right, Vector2 input)
    {
        return forward * input.y + right * input.x;
    }

    public static Vector3 GetTransformVectorDirection(Transform transform, Vector3 input)
    {
        var forward = new Vector3(transform.forward.x, 0, transform.forward.z);
        var right = new Vector3(transform.right.x, 0, transform.right.z);
        var cameraDirection = GetInputDirection(forward, right, input);

        return cameraDirection.normalized;
    }

    public static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}


