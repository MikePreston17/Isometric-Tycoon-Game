using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceTool : Tool {
    public Material m;
    public Mesh cursor;
    void Start() {

    }
    void Update () {
    }
    void OnGUI() {

    }
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(RoundedMousePosition(new Plane(Vector3.up, Vector3.zero),1), Vector3.one * .1f);
    }
}
