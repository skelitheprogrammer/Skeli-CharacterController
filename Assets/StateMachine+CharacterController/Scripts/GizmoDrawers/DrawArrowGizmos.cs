using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public class DrawArrowGizmos : DrawGizmosBase
{
    [SerializeField] private ArrowStruct _arrowSettings;

    protected override void DrawGizmo()
    {
        var position = transform.position;

        var forward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;

        Gizmos.color = _arrowSettings.ArrowColor;
        DrawArrow.ForGizmo(position + _arrowSettings.ArrowOffset, forward, _arrowSettings.ArrowHeadLength, _arrowSettings.ArrowHeadAngle, 1f);

    }
}
#endif
