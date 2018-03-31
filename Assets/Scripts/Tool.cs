using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Tool : MonoBehaviour {
    public readonly Plane standard = new Plane(Vector3.up, Vector3.one);
    public Vector3 RoundedMousePosition(Plane plane, float threshold) {
        float factor = 1 / threshold;
        Vector3 unrounded = UnroundedMousePosition(plane);
        unrounded *= factor;
        unrounded.x = Mathf.Round(unrounded.x);
        unrounded.y = Mathf.Round(unrounded.y);
        unrounded.z = Mathf.Round(unrounded.z);
        unrounded /= factor;
        return unrounded;
    }
    public Vector3 UnroundedMousePosition(Plane plane) { // use with new Plane(Vector3.up, Vector3.zero)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Camera.main.screen
        float distance = 0f;
        plane.Raycast(ray, out distance);
        return ray.GetPoint(distance); 
    }
    public Vector3 Focus(Plane plane) {
        Ray ray = Camera.main.ScreenPointToRay(Vector3.zero);
        float distance = 0f;
        plane.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
