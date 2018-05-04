using UnityEngine;

public abstract class Bezier
{
    public Vector3 Point0 { get; set; }
    public Vector3 Point1 { get; set; }
    public Vector3 Point2 { get; set; }
    public Vector3 Start { get { return Point0; } }
    public abstract Vector3 End { get; }

    public abstract Vector3 Sample(float t);
    // todo: other properties a bezier might have
}
