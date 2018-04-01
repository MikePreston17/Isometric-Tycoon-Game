using ProtoBuf;
using System.IO;
using Terrain;
using UnityEngine;

public class ParkManager
{
    private const string fileExtension = ".park";
    private Park park = Park.Instance;

    public void Save(string filename)
    {
        Save(park, filename);
    }

    public void Save(Park park, string filename)
    {
        string filePath = string.Format("{0}{1}", filename, fileExtension);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        var file = File.Create(filename + fileExtension + "");

        for (int row = 0; row < park.TilesZ; row++)
        {
            for (int column = 0; column < park.TilesX; column++)
            {
                Serializer.Serialize(file, park.TerrainMap[row, column]);
            }
        }

        file.Close();
    }

    public void Load(Park park, string fileName)
    {
        using (var file = File.OpenRead(fileName + fileExtension))
        {
            for (int row = 0; row < park.TilesZ; row++)
            {
                for (int column = 0; column < park.TilesX; column++)
                {
                    park.TerrainMap[row, column] = Serializer.Deserialize<TerrainTile>(file);
                }
            }
        }
        Debug.Log("Loaded file " + fileName + fileExtension);
        park.GenerateAllQuadrantMeshes();
    }

    public void Load(string fileName)
    {
        Load(park, fileName);
    }
}
