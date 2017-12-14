using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAround : MonoBehaviour {

    Vector2 MouseLook;
    Vector2 SmoothV;

    public float Sensitivity;
    public float Smoothing;

    GameObject Character;

	void Start ()
    {
        Character = this.transform.parent.gameObject;
	}
	
	
	void Update ()
    {
        var MouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        MouseDelta = Vector2.Scale(MouseDelta, new Vector2(Sensitivity * Smoothing, Sensitivity * Smoothing));

        SmoothV.x = Mathf.Lerp(SmoothV.x, MouseDelta.x, 1f / Smoothing);
        SmoothV.y = Mathf.Lerp(SmoothV.y, MouseDelta.y, 1f / Smoothing);
        MouseLook += SmoothV;
        MouseLook.y = Mathf.Clamp(MouseLook.y, -40f, 50f);

        transform.localRotation = Quaternion.AngleAxis(-MouseLook.y, Vector3.right);
        Character.transform.localRotation = Quaternion.AngleAxis(MouseLook.x, Character.transform.up);
    }
}
