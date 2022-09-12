using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using TheKiwiCoder;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
     void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView) target;
        Handles.color = UnityEngine.Color.white;
        Handles.DrawWireArc(fov.itself.transform.position, UnityEngine.Vector3.up, UnityEngine.Vector3.forward, 360, fov.viewRadius);
        UnityEngine.Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
        UnityEngine.Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);

        Handles.DrawLine(fov.itself.transform.position, fov.itself.transform.position + viewAngleA * fov.viewRadius);
        Handles.DrawLine(fov.itself.transform.position, fov.itself.transform.position + viewAngleB * fov.viewRadius);

        Handles.color = UnityEngine.Color.red;
        if(fov.visibleTarget != null)
        {
        Handles.DrawLine(fov.itself.transform.position, fov.visibleTarget.position);

        }
        
    }

}
