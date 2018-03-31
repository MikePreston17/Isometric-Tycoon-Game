using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using Terrain;
using UnityEngine;

/// <summary>
/// TODO: Move terrain editing functionality out of this class and into another new class.  All classes should have only one responsibility (SRP).
/// </summary>
public partial class Park : MonoBehaviour
{
    private static bool applicationIsQuitting = false;
    const string fileExtension = ".park";
    public static bool inGame = true;
    public TerrainTile[,] TerrainMap;
    public static Park Instance
    {
        get
        {
            return Singleton<Park>.Instance;
        }
    }
    public int TilesX { get; private set; }
    public int TilesZ { get; private set; }

    [SerializeField]
    public BlockData[,] blocks;
    TerrainQuadrant[] Quadrants;

    private Park() //'private' prevents direct instantiation  (i.e. "new Park()")
    {
        //todo: Move this logic to a Park Builder class (Builder Pattern).  Keep the properties, of course.  Compose and build Park.
        TilesX = 16;
        TilesZ = 16;
        TerrainMap = new TerrainTile[TilesZ, TilesX];

        for (int row = 0; row < TilesZ; row++)
        {
            for (int column = 0; column < TilesX; column++)
            {
                TerrainMap[row, column] = new TerrainTile();
            }
        }
        blocks = new BlockData[TilesZ, TilesX];
    }

    void Awake()
    {
        GenerateQuadrants();
        GenerateBlocks();
        GenerateAllGuadrantMeshes();

        //Load("debug"); //Todo: Park should not have the ability to load itself.  Have another entity create and use the ParkManager I created below.
        new ParkManager().Load(this, "debug");
    }

    //public void InstantiateIfNotAlready() //todo: remove.  Park is a singleton and this functionality is covered
    //{

    //}

    public void GenerateAllGuadrantMeshes()
    {
        GenerateQuadrantMesh(0);
        GenerateQuadrantMesh(1);
        GenerateQuadrantMesh(2);
        GenerateQuadrantMesh(3);
    }

    public TerrainTile GetPatch(int row, int column)
    {
        return (row < 0 || column < 0) ? null : TerrainMap[row, column];  //Todo: Try to never return null if you don't have to, e.g. an empty terrain tile will do.  Null has indeterminate meaning.  "It came back null."  "What does that mean?"  *shrugs*. B^)
    }

    public bool SetPatch(int row, int column, float[] h) //todo: What is 'h'?  Height?
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
    public Vector3 GetTiledVector3(Vector3 orig) {
        throw new NotImplementedException();
    }

    public Vector3[] GetCorneredVector3(int row, int column) {
        Vector3 transform = GetTiledVector3(row, column);
        Vector3[] corners = {
            new Vector3(transform.x - .5f,transform.y,transform.z + .5f),
            new Vector3(transform.x + .5f,transform.y,transform.z + .5f),
            new Vector3(transform.x + .5f,transform.y,transform.z - .5f),
            new Vector3(transform.x - .5f,transform.y,transform.z - .5f)
        };
        return corners;
    }
    public bool Raise(int row, int column, float d0, float d1, float d2, float d3) {
        TerrainTile tile = TerrainMap[row, column];
        if (tile != null)
        {
            tile.h0 += d0;
            tile.h1 += d1;
            tile.h2 += d2;
            tile.h3 += d3;
            GenerateAllGuadrantMeshes();
            return true;
        }
        return false;
    }

    #region Terrain Editor

    //todo: Move these to terrain editor class (SRP)
    public bool AdjustTerrain(int row, int column, float[] heights, bool additive)
    {
        throw new NotImplementedException();

        //LandPatch patch = terrain[row, column];
        //if (patch == null) {
        //    return false;
        //}
        //if (additive) {
        //    for (int i = 0; i < 4; i++) {
        //        patch.h[i] += heights[i];
        //    }
        //} else {
        //    patch.h = heights;
        //}
        //patch.GenerateMesh();
        //return false; // TODO: Put right code here
    }

    public bool AdjustTerrain(int startRow, int startColumn, int endRow, int endColumn, float[] heights, bool additive)
    {
        throw new NotImplementedException();

        //if (endRow > sizeZ)
        //    endRow = sizeZ;
        //if (endColumn > sizeX)
        //    endColumn = sizeZ;
        //float lowest = float.MaxValue;
        //float highest = float.MinValue;
        //LandPatch[,] patches = new LandPatch[endRow - startRow + 1, endColumn - startColumn + 1];
        //for (int row = 0; row < endRow - startRow + 1; row++) {
        //    for (int column = 0; column < endColumn - startColumn + 1; column++) {
        //        patches[row, column] = terrain[startRow + row, startColumn + column];
        //        foreach (float h in patches[row, column].h) {
        //            if (h < lowest) {
        //                lowest = h;
        //            }
        //            if (h > highest) {
        //                highest = h;
        //            }
        //        }
        //    }
        //}
        //if (additive) {
        //    foreach (LandPatch e in patches) {
        //        AdjustTerrain(e.row, e.column, heights, true);
        //    }
        //} else {
        //    if (lowest < highest) {
        //        foreach(LandPatch e in patches) {
        //            // do a "smart" raise
        //        }
        //    }
        //    else {

        //        // do a traditional raise
        //    }


        //}
        //Debug.Log(lowest + " " + highest);
        //return false;
    }
    #endregion Terrain Editor

    #region Park File Functionality

    //todo: move these funcs to a Park Manager class.  Park instances do not need to know how to load themselves (SRP)
    public void Save(string filename) {
        string filePath = string.Format("{0}{1}", filename, fileExtension);
        if (File.Exists(filePath))
            File.Delete(filePath);
        var file = File.Create(filename + fileExtension + "");
        for (int row = 0; row < TilesZ; row++) {
            for (int column = 0; column < TilesX; column++) {
                Serializer.Serialize(file, TerrainMap[row, column]);
            }
        }
        file.Close();
    }

    public void Load(string filename) {
        TerrainTile tile;
        using (var file = File.OpenRead(filename + fileExtension)) {
            for (int row = 0; row < TilesZ; row++) {
                for (int column = 0; column < TilesX; column++) {
                    tile = Serializer.Deserialize<TerrainTile>(file);
                    TerrainMap[tile.row, tile.column] = Serializer.Deserialize<TerrainTile>(file);
                }
            }
        }
        Debug.Log("Loaded file " + filename + fileExtension);
        GenerateAllGuadrantMeshes();
    }
    #endregion Park File Functionality

    #region Quadrant Generation

    //TODO: Move this to a Quadrant Generation class that does quadrant generation.  Park does not need to know HOW to generate its own quadrants, just hold them and maybe use them.  Park is a passive container class, not a builder. (Builder Pattern)

    public void GenerateQuadrants()
    {
        Quadrants = new TerrainQuadrant[4];
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
    public bool IsTerrainLevel(int row, int column) {
        TerrainTile tile = TerrainMap[row, column];
        if (tile.h0 == tile.h1 && tile.h0 == tile.h2 && tile.h0 == tile.h3)
            return true;
        return false;
    }

    public void GenerateQuadrantMesh(int quadrantID) {
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
                if (TerrainMap[r,c].h0 > 0 && TerrainMap[r,c].h1 > 0 && TerrainMap[r,c].h2 > 0 && TerrainMap[r,c].h3 > 0) {
                    normals.Add(Vector3.up);
                    normals.Add(Vector3.up);
                    normals.Add(Vector3.up);
                    normals.Add(Vector3.up);
                }
                else {
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
                if (c < TilesX - 1 && (TerrainMap[r,c].h1 > TerrainMap[r, c+1].h0 || TerrainMap[r,c].h2 > TerrainMap[r,c+1].h3)) { // right cliff face
                    vertices.Add(vertices[ind1]);
                    vertices.Add(vertices[ind2]);
                    vertices.Add(new Vector3(vertices[ind1].x, TerrainMap[r,c+1].h0, vertices[ind1].z));
                    vertices.Add(new Vector3(vertices[ind2].x, TerrainMap[r,c+1].h3, vertices[ind2].z));
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
                if (c > 0 && (TerrainMap[r,c].h0 > TerrainMap[r, c-1].h1 || TerrainMap[r,c].h3 > TerrainMap[r,c-1].h2)) { //left cliff face
                    vertices.Add(vertices[ind0]);
                    vertices.Add(vertices[ind3]);
                    vertices.Add(new Vector3(vertices[ind0].x, TerrainMap[r,c-1].h1, vertices[ind0].z));
                    vertices.Add(new Vector3(vertices[ind3].x, TerrainMap[r,c-1].h2, vertices[ind3].z));
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
                if (r < TilesZ - 1 && (TerrainMap[r,c].h0 > TerrainMap[r+1,c].h0 || TerrainMap[r,c].h1 > TerrainMap[r+1,c].h1)) { // top cliff face
                    vertices.Add(vertices[ind0]);
                    vertices.Add(vertices[ind1]);
                    vertices.Add(new Vector3(vertices[ind0].x, TerrainMap[r+1, c].h0, vertices[ind0].z));
                    vertices.Add(new Vector3(vertices[ind1].x, TerrainMap[r+1, c].h1, vertices[ind1].z));
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
                if (r > 0 && (TerrainMap[r,c].h3 >TerrainMap[r-1,c].h3 || TerrainMap[r,c].h2 > TerrainMap[r-1,c].h2)) {
                    vertices.Add(vertices[ind3]);
                    vertices.Add(vertices[ind2]);
                    vertices.Add(new Vector3(vertices[ind3].x, TerrainMap[r-1,c].h3, vertices[ind3].z));
                    vertices.Add(new Vector3(vertices[ind2].x, TerrainMap[r-1,c].h3, vertices[ind2].z));
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

    #endregion Quadrant Generation

    #region Path Management

    //todo: Move to a Path Manager class.
    public void PlacePath(int row, int column, float height)
    {
        var pathObject = new GameObject();
        Path path = pathObject.AddComponent<Path>();
        path.Initialize(row, column, height);
        blocks[row, column].Add(path);
    }
    #endregion Path Management

    //todo: move to a Park Builder class (Builder Pattern)
    public void GenerateBlocks()
    {
        for (int r = 0; r < TilesX; r++)
        {
            for (int c = 0; c < TilesZ; c++)
            {
                blocks[r, c] = new BlockData();
            }
        }
    }

    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}

