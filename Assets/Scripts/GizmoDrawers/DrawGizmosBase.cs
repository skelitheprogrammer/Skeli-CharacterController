using System.Diagnostics;
using UnityEngine;

#if UNITY_EDITOR
public abstract class DrawGizmosBase : MonoBehaviour, IDrawGizmo
{
    [field: SerializeField] public bool Enabled { get; protected set; }
    
    public abstract void DrawGizmo();
    private void Draw()
    {
        if (!Enabled) return;

        DrawGizmo();
    }

    protected void OnDrawGizmos()
    {
        Draw();
    }
}

#endif
