using System;
using UnityEngine;

public class PathfindingAction : Node
{
    Vector3 start, end;

    public new void Initialize(GameObject g, params object[] parameters)
    {
        start = (Vector3)parameters[0];
        end = (Vector3)parameters[1];
    }

    public override Status Tick(float deltaTime)
    {
        throw new NotImplementedException();
    }
}
