using System.Collections.Generic;
using UnityEngine;

public class Buildable : MonoBehaviour
{
}

public abstract class Block : Buildable
{
    public int Row { get; set; }
    public int Column { get; set; }

    public List<Block> GetNeighbors()
    {
        return null;
    }
}
