using UnityEngine;

public abstract class Bezier : IBezier
{
    public Vector3 Point1 { get; set; }
    public Vector3 Point2 { get; set; }
    public Vector3 Point3 { get; set; }
    public Vector3 Start { get { return Point1; } }
    public abstract Vector3 End { get; }

    public abstract Vector3 Sample(float t);
    // todo: other properties a bezier might have
}
