using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : MonoBehaviour {

    public static void DrawRay( Vector3 position, Vector3 direction, Color color ) {
        if (direction.sqrMagnitude > 0.001f) {
            Debug.DrawRay(position, direction, color);
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawSolidDisc(position + direction, Vector3.up, 0.25f);
        }
    }
    public static void DrawLabel( Vector3 position, string Label, Color color ) {
        UnityEditor.Handles.BeginGUI();
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.Label(position, Label);
        UnityEditor.Handles.EndGUI();
    }
    public static void DrawCurrentDestiny( Vector3 position, Color color, float DiscSize = 0.25f ) {
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.DrawSolidDisc(position, Vector3.up, DiscSize);
    }
    public static void DrawCircle( Vector3 position, Color color, float CirleSize ) {
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.DrawWireDisc(position, Vector3.up, CirleSize);
    }
    public static void DrawSolidSphere( Vector3 position, Color color, float size ) {
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.DrawSolidDisc(position, Vector3.up, size);
    }
    public static void DrawWireSphere( Vector3 position, Color color, float size ) {
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.DrawWireDisc(position, Vector3.up, size);
    }
    public static Collider GetClosestEnemy(Vector3 CurrentPosition,  Collider[] enemies ) {
        Collider tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = CurrentPosition;
        foreach (Collider t in enemies) {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist) {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }


}
