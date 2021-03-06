﻿using UnityEngine;

[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public partial class BezierFollower : MonoBehaviour
{
    /* scratchpad */
    private MeshFilter meshFilter;
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        var mesh = GetMesh();
        var shape = GetExtrudeShape();
        var path = GetPath();

        Extrude(mesh, shape, path);
    }

    private ExtrudeShape GetExtrudeShape()
    {
        var vert2Ds = new Vertex[] {
                new Vertex(
                    new Vector3(0, -1, 0),
                    new Vector3(0, -1, 0),
                    0),
                new Vertex(
                    new Vector3(0.58779f, -0.80902f, 0),
                    new Vector3(0, -1, 0),
                    0.5f),
                new Vertex(
                    new Vector3(0.95106f, -0.30901f, 0),
                    new Vector3(0, -1, 0),
                    0.5f),
                new Vertex(
                    new Vector3(0.95106f, 0.30901f, 0),
                    new Vector3(0, -1, 0),
                    1),
                new Vertex(
                    new Vector3(0.58779f,0.80902f,0),
                    new Vector3(0,-1,0),
                    1),
                new Vertex(
                    new Vector3(0,1,0),
                    new Vector3(0,-1,0),
                    1),
                new Vertex(
                    new Vector3(-0.58779f,0.80902f,0),
                    new Vector3(0,-1,0),
                    1),
                new Vertex(
                    new Vector3(-0.95106f,0.30901f,0),
                    new Vector3(0,-1,0),
                    1),
                new Vertex(
                    new Vector3(-0.95106f,-0.30901f,0),
                    new Vector3(0,-1,0),
                    1),
                new Vertex(
                    new Vector3(-0.58779f,-0.80902f,0),
                    new Vector3(0,-1,0),
                    1),
            };

        var lines = new int[] {
                0, 1,
                1, 2,
                2, 3,
                3, 4,
                4, 5,
                5, 6,
                6, 7,
                7, 8,
                8, 9,
                9, 0
            };

        return new ExtrudeShape(vert2Ds, lines);
    }

    private OrientedPoint[] GetPath()
    {
        var p = new Vector3[] {
                new Vector3(0, 0, 0),
                new Vector3(5, -2, 5),
                new Vector3(5, -4, 0),
                new Vector3(0, -6, 0)
            };

        var path = new System.Collections.Generic.List<OrientedPoint>();

        for (float t = 0; t <= 1; t += 0.1f)
        {
            var point = GetPoint(p, t);
            var rotation = GetOrientation3D(p, t, Vector3.up);
            path.Add(new OrientedPoint(point, rotation));
        }

        return path.ToArray();
    }

    private Mesh GetMesh()
    {
        if (meshFilter.sharedMesh == null)
        {
            meshFilter.sharedMesh = new Mesh();
        }
        return meshFilter.sharedMesh;
    }

    private Vector3 GetPoint(Vector3[] p, float t)
    {
        float omt = 1f - t;
        float omt2 = omt * omt;
        float t2 = t * t;
        return
            p[0] * (omt2 * omt) +
            p[1] * (3f * omt2 * t) +
            p[2] * (3f * omt * t2) +
            p[3] * (t2 * t);
    }

    private Vector3 GetTangent(Vector3[] p, float t)
    {
        float omt = 1f - t;
        float omt2 = omt * omt;
        float t2 = t * t;
        Vector3 tangent =
            p[0] * (-omt2) +
            p[1] * (3 * omt2 - 2 * omt) +
            p[2] * (-3 * t2 + 2 * t) +
            p[3] * (t2);
        return tangent.normalized;
    }

    private Vector3 GetNormal3D(Vector3[] p, float t, Vector3 up)
    {
        var tng = GetTangent(p, t);
        var binormal = Vector3.Cross(up, tng).normalized;
        return Vector3.Cross(tng, binormal);
    }

    private Quaternion GetOrientation3D(Vector3[] p, float t, Vector3 up)
    {
        var tng = GetTangent(p, t);
        var nrm = GetNormal3D(p, t, up);
        return Quaternion.LookRotation(tng, nrm);
    }

    private void Extrude(Mesh mesh, ExtrudeShape shape, OrientedPoint[] path)
    {
        int vertsInShape = shape.vert2Ds.Length;
        int segments = path.Length - 1;
        int edgeLoops = path.Length;
        int vertCount = vertsInShape * edgeLoops;
        int triCount = shape.lines.Length * segments;
        int triIndexCount = triCount * 3;

        var triangleIndices = new int[triIndexCount];
        var vertices = new Vector3[vertCount];
        var normals = new Vector3[vertCount];
        var uvs = new Vector2[vertCount];

        float totalLength = 0;
        float distanceCovered = 0;
        for (int i = 0; i < path.Length - 1; i++)
        {
            var d = Vector3.Distance(path[i].Position, path[i + 1].Position);
            totalLength += d;
        }

        for (int i = 0; i < path.Length; i++)
        {
            int offset = i * vertsInShape;
            if (i > 0)
            {
                var d = Vector3.Distance(path[i].Position, path[i - 1].Position);
                distanceCovered += d;
            }
            float v = distanceCovered / totalLength;

            for (int j = 0; j < vertsInShape; j++)
            {
                int id = offset + j;
                vertices[id] = path[i].LocalToWorld(shape.vert2Ds[j].Point);
                normals[id] = path[i].LocalToWorldDirection(shape.vert2Ds[j].Normal);
                uvs[id] = new Vector2(shape.vert2Ds[j].uCoord, v);
            }
        }
        int ti = 0;
        for (int i = 0; i < segments; i++)
        {
            int offset = i * vertsInShape;
            for (int l = 0; l < shape.lines.Length; l += 2)
            {
                int a = offset + shape.lines[l] + vertsInShape;
                int b = offset + shape.lines[l];
                int c = offset + shape.lines[l + 1];
                int d = offset + shape.lines[l + 1] + vertsInShape;
                triangleIndices[ti] = c; ti++;
                triangleIndices[ti] = b; ti++;
                triangleIndices[ti] = a; ti++;
                triangleIndices[ti] = a; ti++;
                triangleIndices[ti] = d; ti++;
                triangleIndices[ti] = c; ti++;
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.triangles = triangleIndices;

        //mesh.RecalculateBounds();
        //mesh.RecalculateTangents();
        //mesh.RecalculateNormals();
    }
}
