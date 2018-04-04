﻿using System;
using UnityEngine;

public class Bezier3 : Bezier
{
    public override Vector3 End { get { return Point3; } }

    public override Vector3 Sample(float t)
    {
        throw new NotImplementedException(); // todo: quadratic bezier sampling
    }

    public Bezier3()
    {
        Point1 = Vector3.zero;
        Point2 = Vector3.zero;
        Point3 = Vector3.zero;
    }

    public Bezier3(Vector3 point1, Vector3 point2, Vector3 point3)
    {
        Point1 = point1;
        Point2 = point2;
        Point3 = point3;
    }
}
