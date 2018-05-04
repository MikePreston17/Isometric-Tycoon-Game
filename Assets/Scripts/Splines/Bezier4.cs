using System;
using UnityEngine;

public class Bezier4 : Bezier
{
    public Vector3 Point3;
    public override Vector3 End { get { return Point3; } }
    public override Vector3 Sample(float t)
    {
        float omt = 1f - t;
        float omt2 = omt * omt;
        float t2 = t * t;
        return
            Point0 * (omt2 * omt) +
            Point1 * (3f * omt2 * t) +
            Point2 * (3f * omt * t2) +
            Point3 * (t2 * t);
    }
    public Bezier4()
    {
        Point0 = Vector3.zero;
        Point1 = Vector3.zero;
        Point2 = Vector3.zero;
        Point3 = Vector3.zero;
    }
    public Bezier4(Vector3 Point0, Vector3 Point1, Vector3 Point2, Vector3 Point3)
    {
        this.Point0 = Point0;
        this.Point1 = Point1;
        this.Point2 = Point2;
        this.Point3 = Point3;
    }
}
