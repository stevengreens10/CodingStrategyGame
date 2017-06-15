using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace CSharpRunner
{
    public class Cell : MapObject
    {

        private Location location;

        [JsonProperty()]
        internal int[] Walls { get; set; } // top down right left;

        [JsonIgnore()]
        public bool[] WallsArray { get { return Walls.Select(x => System.Convert.ToBoolean(x)).ToArray(); } }

        internal Cell[] Neighbors { get; private set; } // top down right left;

        public int Visited { get; internal set; }

        private List<MapObject> Contents { get; set; }


        [JsonProperty()]
        private int x, y;

        public Cell(Location loc, int size)
        {
            location = loc;
            Walls = new int[4];
            Neighbors = new Cell[4];

            for (int i = 0; i < Walls.Length; i++)
                Walls[i] = 1;

            Visited = 0;
            Contents = new List<MapObject>();
            x = loc.X;
            y = loc.Y;
        }

        public override Location GetLocation() => location;
            
        

    }
}
