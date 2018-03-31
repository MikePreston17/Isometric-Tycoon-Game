using System.Collections.Generic;
using UnityEngine;

public class Buildable : MonoBehaviour
{
}

public abstract class Block : Buildable
{
    public int row { get; set; }
    public int column { get; set; }

    public List<Block> GetNeighbors()
    {
        return null;
    }
}

public class BlockData
{
    List<Block> blocks;

    public BlockData()
    {
        blocks = new List<Block>();
    }
    public void Add(Block block)
    {
        blocks.Add(block);
        // TODO: Expand to check for height-related conflicts
    }

    private bool CheckForConflicts()
    {
        return true;
    }
}
