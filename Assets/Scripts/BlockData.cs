using System;
using System.Collections.Generic;
using System.Linq;

public class BlockData
{
    private List<Block> _blocks = new List<Block>(0); //todo: @Mike - Change this to a double array of Blocks and index it.

    public BlockData() { }

    public Block this[int index]
    {
        get { return _blocks[index]; }
        set
        {
            if (value != null)
            {
                // TODO: Expand to check for height-related conflicts
                _blocks.Add(value);
            }
        }
    }

    public void Add(params Block[] blocks)
    {
        if (blocks == null || !blocks.Any())
        {
            return;
        }

        _blocks.AddRange(blocks);
    }

    public void Clear()
    {
        _blocks.Clear();
    }

    public bool Contains(object value)
    {
        throw new NotImplementedException();
    }
}
