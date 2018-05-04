using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierFollower2 : MonoBehaviour {
    Bezier4 curve;
    LineRenderer lineRenderer;
    private void Awake() {
        curve = new Bezier4(
            new Vector3(0, 1, 0), 
            new Vector3(0, .66f, .66f), 
            new Vector3(0, .33f, .33f), 
            new Vector3(0, 0, 1));
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 30;
    }
    private void Update() {
        float t = 0;
        for (int i = 0; i < 30; i++) {
            t = (i+1) * 1/30f;
            lineRenderer.SetPosition(i, curve.Sample(t));
        }





    }

    private void OnDrawGUI() {
        if (Application.isPlaying) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(curve.Point0, .3f);
            Gizmos.DrawWireSphere(curve.Point1, .3f);
            Gizmos.DrawWireSphere(curve.Point2, .3f);
            Gizmos.DrawWireSphere(curve.Point3, .3f);
        }
    }
}
