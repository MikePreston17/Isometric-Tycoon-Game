using ProtoBuf;

namespace Terrain
{
    [ProtoContract]
    public class TerrainTile {
        [ProtoMember(0, IsRequired = true)]
        public int row { get; set; }
        [ProtoMember(1, IsRequired = true)]
        public int column { get; set; }
        [ProtoMember(2, IsRequired = true)]
        public float h0;
        [ProtoMember(3, IsRequired = true)]
        public float h1;
        [ProtoMember(4, IsRequired = true)]
        public float h2;
        [ProtoMember(5, IsRequired = true)]
        public float h3;
        public TerrainTile()
        {
            row = 0;
            column = 0;
            h0 = 1;
            h1 = 1;
            h2 = 1;
            h3 = 1;
        }
    }
}