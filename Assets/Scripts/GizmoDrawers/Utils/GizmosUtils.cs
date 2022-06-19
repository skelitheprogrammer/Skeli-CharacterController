using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public static class GizmosUtils
{
    public static void DrawWireCapsule(Vector3 position, float height, float radius)
    {
        using (new Handles.DrawingScope(Gizmos.color, Gizmos.matrix))
        {
            var topPos = position + Vector3.up * (height /2 - radius);
            var botPos = position + Vector3.down * (height /2 - radius);

            var offsetX = new Vector3(radius, 0f, 0f);
            var offsetZ = new Vector3(0f, 0f, radius);

            Handles.DrawWireArc(topPos, Vector3.back, Vector3.left, 180, radius);
            Handles.DrawLine(botPos + offsetX, topPos + offsetX);
            Handles.DrawLine(botPos - offsetX, topPos - offsetX);
            Handles.DrawWireArc(botPos, Vector3.back, Vector3.left, -180, radius);

            Handles.DrawWireArc(topPos, Vector3.left, Vector3.back, -180, radius);
            Handles.DrawLine(botPos + offsetZ, topPos + offsetZ);
            Handles.DrawLine(botPos - offsetZ, topPos - offsetZ);
            Handles.DrawWireArc(botPos, Vector3.left, Vector3.back, 180, radius);

            Handles.DrawWireDisc(topPos, Vector3.up, radius);
            Handles.DrawWireDisc(botPos, Vector3.up, radius);
        }
    }
}
#endif