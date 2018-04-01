using System;
using Terrain;
using UnityEngine;

/// <summary>
/// TODO: Move terrain editing functionality out of this class and into another new class.  All classes should have only one responsibility (SRP).
/// </summary>
public partial class Park : MonoBehaviour
{
    private static bool applicationIsQuitting = false;
    public static bool inGame = true;
    public TerrainTile[,] TerrainMap;
    QuadrantGenerator quadrantGenerator;

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
    public TerrainQuadrant[] Quadrants;

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

    internal void GenerateAllQuadrantMeshes()
    {
        quadrantGenerator.GenerateAllQuadrantMeshes();
    }

    void Awake()
    {
        Debug.Log("Park awake.");
        quadrantGenerator = new QuadrantGenerator(this);
        quadrantGenerator.GenerateQuadrants();
        GenerateBlocks();
        quadrantGenerator.GenerateAllQuadrantMeshes();

        //Load("debug"); //Todo: Park should not have the ability to load itself.  Have another entity create and use the ParkManager I created below.
        new ParkManager().Load(this, "debug");
    }

    public TerrainTile GetPatch(int row, int column)
    {
        return (row < 0 || column < 0) ? null : TerrainMap[row, column];  //Todo: Try to never return null if you don't have to, e.g. an empty terrain tile will do.  Null has indeterminate meaning.  "It came back null."  "What does that mean?"  *shrugs*. B^)
    }

    public bool SetPatch(int row, int column, float[] h) //todo: What is 'h'?  Height?
    {
        throw new NotImplementedException();
    }

    public bool Raise(int row, int column, float d0, float d1, float d2, float d3)
    {
        TerrainTile tile = TerrainMap[row, column];

        if (tile != null)
        {
            tile.h0 += d0;
            tile.h1 += d1;
            tile.h2 += d2;
            tile.h3 += d3;
            quadrantGenerator.GenerateAllQuadrantMeshes();
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
