using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicKeyboard : MonoBehaviour
{
    [Header("Steering settings")]
    public float _maxSpeed;

    [Header("Steering runtime")]
    public Vector3 _position = Vector3.zero;
    public Vector3 _velocity = Vector3.zero;

    private void Start() {
        _position = transform.position;
    }

    void FixedUpdate()
    {
      
        Vector3 Direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        _velocity = Direction.normalized * _maxSpeed;
        _position = _position + _velocity * Time.deltaTime;

        transform.position = _position;
        transform.LookAt(_position + _velocity.normalized);
    }

    private void OnDrawGizmos() {
        DrawRay(transform.position, _velocity, Color.red);
        DrawLabel(transform.position, name, Color.red);
    }

    static void DrawRay(Vector3 position, Vector3 direction, Color color ) {
        if (direction.sqrMagnitude > 0.001f) {
            Debug.DrawRay(position, direction, color);
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawSolidDisc(position + direction, Vector3.up, 0.25f);
        }
    }
    static void DrawLabel(Vector3 position, string Label, Color color) {
        UnityEditor.Handles.BeginGUI();
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.Label(position, Label);
        UnityEditor.Handles.EndGUI();
    }

}
