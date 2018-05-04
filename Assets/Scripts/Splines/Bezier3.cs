using System;
using UnityEngine;

public class Bezier3 : Bezier
{
    public override Vector3 End { get { return Point2; } }

    public override Vector3 Sample(float t)
    {
        throw new NotImplementedException(); // todo: quadratic bezier sampling
    }

    public Bezier3()
    {
        Point0 = Vector3.zero;
        Point1 = Vector3.zero;
        Point2 = Vector3.zero;
    }

    public Bezier3(Vector3 point1, Vector3 point2, Vector3 point3)
    {
        Point0 = point1;
        Point1 = point2;
        Point2 = point3;
    }
}
