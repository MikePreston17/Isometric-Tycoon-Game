using UnityEngine;

public struct Vertex
{
    public Vector3 Point;
    public Vector3 Normal;
    public float uCoord; //todo: What is uCoord?  "bitCoordinate"?  Maybe try a rename.  -MP

    public Vertex(Vector3 point, Vector3 normal, float uCoord)
    {
        Point = point;
        Normal = normal;
        this.uCoord = uCoord;
    }
}

