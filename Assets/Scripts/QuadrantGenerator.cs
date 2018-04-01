using System;
using System.Collections.Generic;
using Terrain;
using UnityEngine;

public class QuadrantGenerator
{
    private Park park;
    private TerrainQuadrant[] Quadrants
    {
        get
        {
            return park.Quadrants;
        }
        set { park.Quadrants = value; }
    }
    private int TilesZ { get { return park.TilesZ; } }
    private int TilesX { get { return park.TilesX; } }
    public TerrainTile[,] TerrainMap { get { return park.TerrainMap; } }

    public QuadrantGenerator(Park park)
    {
        this.park = park;
        Quadrants = new TerrainQuadrant[4];
    }

    //TerrainQuadrant Quadrants[,] {get{return park.Quadrants;}

    public void GenerateQuadrants()
    {
        Quadrants[0] = new GameObject("Quadrant North-West").AddComponent<TerrainQuadrant>();
        Quadrants[1] = new GameObject("Quadrant North-East").AddComponent<TerrainQuadrant>();
        Quadrants[2] = new GameObject("Quadrant South-West").AddComponent<TerrainQuadrant>();
        Quadrants[3] = new GameObject("Quadrant South-East").AddComponent<TerrainQuadrant>();

        Quadrants[0].LeftBoundary = 0;
        Quadrants[2].LeftBoundary = 0;
        Quadrants[1].LeftBoundary = TilesZ / 2;
        Quadrants[3].LeftBoundary = TilesZ / 2;
        Quadrants[1].RightBoundary = TilesX - 1;
        Quadrants[3].RightBoundary = TilesX - 1;
        Quadrants[0].RightBoundary = (TilesX / 2) - 1;
        Quadrants[2].RightBoundary = (TilesX / 2) - 1;
        Quadrants[2].BottomBoundary = 0;
        Quadrants[3].BottomBoundary = 0;
        Quadrants[0].BottomBoundary = (TilesZ / 2);
        Quadrants[1].BottomBoundary = (TilesZ / 2);
        Quadrants[0].TopBoundary = TilesZ - 1;
        Quadrants[1].TopBoundary = TilesZ - 1;
        Quadrants[2].TopBoundary = (TilesZ / 2) - 1;
        Quadrants[3].TopBoundary = (TilesZ / 2) - 1;
        GenerateQuadrantMesh(0);
    }

    public void GenerateAllQuadrantMeshes()
    {
        GenerateQuadrantMesh(0);
        GenerateQuadrantMesh(1);
        GenerateQuadrantMesh(2);
        GenerateQuadrantMesh(3);
    }

    public bool IsTerrainLevel(int row, int column)
    {
        TerrainTile tile = TerrainMap[row, column];

        return (tile.h0 == tile.h1 && tile.h0 == tile.h2 && tile.h0 == tile.h3);
    }

    public void GenerateQuadrantMesh(int quadrantID)
    {
        Mesh mesh = Quadrants[quadrantID].GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        List<Vector3> vertices = new List<Vector3>();
        vertices.Clear();
        List<Vector3> normals = new List<Vector3>();
        normals.Clear();
        List<Vector2> uvs = new List<Vector2>();
        uvs.Clear();
        List<int> triangles = new List<int>();
        triangles.Clear();
        Vector3[] corners;
        int ind0, ind1, ind2, ind3;
        for (int r = Quadrants[quadrantID].BottomBoundary; r <= Quadrants[quadrantID].TopBoundary; r++)
        {
            for (int c = Quadrants[quadrantID].LeftBoundary; c <= Quadrants[quadrantID].RightBoundary; c++)
            {
                corners = GetCorneredVector3(r, c);
                //h = terrain[r, c].h;
                //Vector3 tiled = GetTiledVector3(r, c);
                vertices.Add(new Vector3(corners[0].x, TerrainMap[r, c].h0, corners[1].z));
                vertices.Add(new Vector3(corners[1].x, TerrainMap[r, c].h1, corners[1].z));
                vertices.Add(new Vector3(corners[2].x, TerrainMap[r, c].h2, corners[2].z));
                vertices.Add(new Vector3(corners[3].x, TerrainMap[r, c].h3, corners[3].z));
                // store the indexes of the top verts so we can make future triangles
                uvs.Add(new Vector2(0, 0));
                uvs.Add(new Vector2(0, 1));
                uvs.Add(new Vector2(1, 0));
                uvs.Add(new Vector2(1, 1));
                // temp uvs, will make better later
                ind0 = vertices.Count - 4;
                ind1 = vertices.Count - 3;
                ind2 = vertices.Count - 2;
                ind3 = vertices.Count - 1;
                // top normals
                // todo: normals aren't always up, depending upon how the terrain is angled
                if (TerrainMap[r, c].h0 > 0 && TerrainMap[r, c].h1 > 0 && TerrainMap[r, c].h2 > 0 && TerrainMap[r, c].h3 > 0)
                {
                    normals.Add(Vector3.up);
                    normals.Add(Vector3.up);
                    normals.Add(Vector3.up);
                    normals.Add(Vector3.up);
                }
                else
                {
                    normals.Add(Vector3.down);
                    normals.Add(Vector3.down);
                    normals.Add(Vector3.down);
                    normals.Add(Vector3.down);
                }
                // top triangles
                triangles.Add(ind0);
                triangles.Add(ind1);
                triangles.Add(ind2);
                triangles.Add(ind2);
                triangles.Add(ind3);
                triangles.Add(ind0);
                if (c == 0)
                { // furthest west boundary
                    vertices.Add(vertices[ind0]); // match top edge
                    vertices.Add(vertices[ind3]);
                    vertices.Add(new Vector3(vertices[ind0].x, 0, vertices[ind0].z));
                    vertices.Add(new Vector3(vertices[ind3].x, 0, vertices[ind3].z));
                    triangles.Add(vertices.Count - 4);
                    triangles.Add(vertices.Count - 3);
                    triangles.Add(vertices.Count - 1);
                    triangles.Add(vertices.Count - 1);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 4);
                    normals.Add(Vector3.left);
                    normals.Add(Vector3.left);
                    normals.Add(Vector3.left);
                    normals.Add(Vector3.left);
                }
                if (c == Park.Instance.TilesX - 1)
                { // furthest east boundary
                    vertices.Add(vertices[ind1]);
                    vertices.Add(vertices[ind2]);
                    vertices.Add(new Vector3(vertices[ind1].x, 0, vertices[ind1].z));
                    vertices.Add(new Vector3(vertices[ind2].x, 0, vertices[ind2].z));
                    triangles.Add(vertices.Count - 3);
                    triangles.Add(vertices.Count - 4);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 1);
                    triangles.Add(vertices.Count - 3);
                    normals.Add(Vector3.right);
                    normals.Add(Vector3.right);
                    normals.Add(Vector3.right);
                    normals.Add(Vector3.right);
                }
                if (r == Park.Instance.TilesZ - 1)
                { // furthest north boundary
                    vertices.Add(vertices[ind0]);
                    vertices.Add(vertices[ind1]);
                    vertices.Add(new Vector3(vertices[ind0].x, 0, vertices[ind0].z));
                    vertices.Add(new Vector3(vertices[ind1].x, 0, vertices[ind1].z));
                    triangles.Add(vertices.Count - 3);
                    triangles.Add(vertices.Count - 4);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 1);
                    triangles.Add(vertices.Count - 3);
                    normals.Add(Vector3.forward);
                    normals.Add(Vector3.forward);
                    normals.Add(Vector3.forward);
                    normals.Add(Vector3.forward);
                }
                if (r == 0)
                { // furthest south boundary
                    vertices.Add(vertices[ind3]);
                    vertices.Add(vertices[ind2]);
                    vertices.Add(new Vector3(vertices[ind3].x, 0, vertices[ind3].z));
                    vertices.Add(new Vector3(vertices[ind2].x, 0, vertices[ind2].z));
                    triangles.Add(vertices.Count - 4);
                    triangles.Add(vertices.Count - 3);
                    triangles.Add(vertices.Count - 1);
                    triangles.Add(vertices.Count - 1);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 4);
                    normals.Add(Vector3.back);
                    normals.Add(Vector3.back);
                    normals.Add(Vector3.back);
                    normals.Add(Vector3.back);
                }
                if (c < TilesX - 1 && (TerrainMap[r, c].h1 > TerrainMap[r, c + 1].h0 || TerrainMap[r, c].h2 > TerrainMap[r, c + 1].h3))
                { // right cliff face
                    vertices.Add(vertices[ind1]);
                    vertices.Add(vertices[ind2]);
                    vertices.Add(new Vector3(vertices[ind1].x, TerrainMap[r, c + 1].h0, vertices[ind1].z));
                    vertices.Add(new Vector3(vertices[ind2].x, TerrainMap[r, c + 1].h3, vertices[ind2].z));
                    triangles.Add(vertices.Count - 3);
                    triangles.Add(vertices.Count - 4);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 1);
                    triangles.Add(vertices.Count - 3);
                    normals.Add(Vector3.right);
                    normals.Add(Vector3.right);
                    normals.Add(Vector3.right);
                    normals.Add(Vector3.right);
                }
                if (c > 0 && (TerrainMap[r, c].h0 > TerrainMap[r, c - 1].h1 || TerrainMap[r, c].h3 > TerrainMap[r, c - 1].h2))
                { //left cliff face
                    vertices.Add(vertices[ind0]);
                    vertices.Add(vertices[ind3]);
                    vertices.Add(new Vector3(vertices[ind0].x, TerrainMap[r, c - 1].h1, vertices[ind0].z));
                    vertices.Add(new Vector3(vertices[ind3].x, TerrainMap[r, c - 1].h2, vertices[ind3].z));
                    triangles.Add(vertices.Count - 4);
                    triangles.Add(vertices.Count - 3);
                    triangles.Add(vertices.Count - 1);
                    triangles.Add(vertices.Count - 1);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 4);
                    normals.Add(Vector3.left);
                    normals.Add(Vector3.left);
                    normals.Add(Vector3.left);
                    normals.Add(Vector3.left);
                }
                if (r < TilesZ - 1 && (TerrainMap[r, c].h0 > TerrainMap[r + 1, c].h0 || TerrainMap[r, c].h1 > TerrainMap[r + 1, c].h1))
                { // top cliff face
                    vertices.Add(vertices[ind0]);
                    vertices.Add(vertices[ind1]);
                    vertices.Add(new Vector3(vertices[ind0].x, TerrainMap[r + 1, c].h0, vertices[ind0].z));
                    vertices.Add(new Vector3(vertices[ind1].x, TerrainMap[r + 1, c].h1, vertices[ind1].z));
                    triangles.Add(vertices.Count - 3);
                    triangles.Add(vertices.Count - 4);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 1);
                    triangles.Add(vertices.Count - 3);
                    normals.Add(Vector3.forward);
                    normals.Add(Vector3.forward);
                    normals.Add(Vector3.forward);
                    normals.Add(Vector3.forward);
                }
                if (r > 0 && (TerrainMap[r, c].h3 > TerrainMap[r - 1, c].h3 || TerrainMap[r, c].h2 > TerrainMap[r - 1, c].h2))
                {
                    vertices.Add(vertices[ind3]);
                    vertices.Add(vertices[ind2]);
                    vertices.Add(new Vector3(vertices[ind3].x, TerrainMap[r - 1, c].h3, vertices[ind3].z));
                    vertices.Add(new Vector3(vertices[ind2].x, TerrainMap[r - 1, c].h3, vertices[ind2].z));
                    triangles.Add(vertices.Count - 4);
                    triangles.Add(vertices.Count - 3);
                    triangles.Add(vertices.Count - 1);
                    triangles.Add(vertices.Count - 1);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 4);
                    normals.Add(Vector3.back);
                    normals.Add(Vector3.back);
                    normals.Add(Vector3.back);
                    normals.Add(Vector3.back);
                }
            }
        }
        mesh.vertices = vertices.ToArray();
        mesh.normals = normals.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
        Quadrants[quadrantID].GetComponent<MeshRenderer>().material = (Material)Resources.Load("Terrain");
        Quadrants[quadrantID].GetComponent<MeshCollider>().sharedMesh = null;
        Quadrants[quadrantID].GetComponent<MeshCollider>().sharedMesh = mesh;
        Quadrants[quadrantID].GetComponent<MeshCollider>().convex = true;
    }

    public Vector3[] GetCorneredVector3(int row, int column)
    {
        Vector3 transform = GetTiledVector3(row, column);
        Vector3[] corners =
        {
            new Vector3(transform.x - .5f,transform.y,transform.z + .5f),
            new Vector3(transform.x + .5f,transform.y,transform.z + .5f),
            new Vector3(transform.x + .5f,transform.y,transform.z - .5f),
            new Vector3(transform.x - .5f,transform.y,transform.z - .5f)
        };
        return corners;
    }

    public Vector3 GetTiledVector3(Vector3 orig)
    {
        throw new NotImplementedException();
    }

    public Vector3 GetTiledVector3(int row, int column)
    {
        int xOffset = column - (TilesX / 2);
        int zOffset = row - (TilesZ / 2);
        Vector3 transform = new Vector3(.5f + xOffset, 0, .5f + zOffset);
        return transform;
    }
}
