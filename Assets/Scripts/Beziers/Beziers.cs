using UnityEngine;

public static class Beziers //todo: name as something a bit more descriptive.  What does it do?  -MP
{
    public static readonly Bezier3 straight_down_quarter = new Bezier3(new Vector3(1, 1, 0), new Vector3(0.5f, 0.5f, 0), Vector3.zero);
}
