using UnityEngine;

public interface IBezier //@Jacob I have added this and it's optional.  -MP
{
    Vector3 Point1 { get; set; }
    Vector3 Point2 { get; set; }
    Vector3 Point3 { get; set; }
    Vector3 Start { get; }
    Vector3 End { get; }

    Vector3 Sample(float t);
}
