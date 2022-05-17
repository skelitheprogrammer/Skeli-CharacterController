using UnityEngine;

#if UNITY_EDITOR
public abstract class DrawGizmosBase : MonoBehaviour
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
#endif