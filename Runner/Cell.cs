using System.Collections.Generic;
using Newtonsoft.Json;

namespace CSharpRunner
{
    public class Cell : MapObject
    {

        private Location location;
        public bool[] Walls { get; private set; } // top down right left;
        internal Cell[] Neighbors { get; private set; } // top down right left;
        public int Visited { get; internal set; }
        public List<MapObject> Contents { get; private set; }

        [JsonProperty()]
        private int x, y;

        public Cell(Location loc, int size)
        {
            location = loc;
            Walls = new bool[4];
            Neighbors = new Cell[4];

            for (int i = 0; i < Walls.Length; i++)
                Walls[i] = true;

            Visited = 0;
            Contents = new List<MapObject>();
            x = loc.X;
            y = loc.Y;
        }

        public override Location GetLocation() => location;

    }
}
