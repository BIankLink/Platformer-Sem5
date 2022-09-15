using System.Collections;
using System.Collections.Generic;
using UnityEditor;


[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
     void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView) target;
        Handles.color = UnityEngine.Color.white;
        Handles.DrawWireArc(fov.transform.position, UnityEngine.Vector3.up, UnityEngine.Vector3.forward, 360, fov.viewRadius);
        UnityEngine.Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
        UnityEngine.Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

        Handles.color = UnityEngine.Color.red;
        
        if(fov.visibleTarget != null)
        {
        Handles.DrawLine(fov.transform.position, fov.visibleTarget.position);

        }
        
    }

}
