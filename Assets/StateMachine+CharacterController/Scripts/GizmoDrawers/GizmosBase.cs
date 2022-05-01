using UnityEngine;

public abstract class GizmosBase : MonoBehaviour
{
    [SerializeField] protected bool _enabled = true;

    protected abstract void DrawGizmo();
    private void Draw()
    {
        if (!_enabled) return;

        DrawGizmo();
    }

    protected void OnDrawGizmos()
    {
        Draw();
    }
}
