using System;
using UnityEngine;

public class Bezier4 : Bezier
{
    public Vector3 Point4;
    public override Vector3 End { get { return Point4; } }
    public override Vector3 Sample(float t)
    {
        throw new NotImplementedException(); // todo: cubic bezier sampling
    }
    public Bezier4()
    {
        Point1 = Vector3.zero;
        Point2 = Vector3.zero;
        Point3 = Vector3.zero;
        Point4 = Vector3.zero;
    }
    public Bezier4(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        Point1 = p1;
        Point2 = p2;
        Point3 = p3;
        this.Point4 = p4;
    }
}
