using System.Collections.Generic;

namespace CSharpRunner
{
    public class Cell : MapObject
    {
        private Location location;
        public Wall[] Walls { get; private set; } // top down right left;
        public Cell[] Neighbors { get; private set; } // top down right left;
        public int Visited { get; internal set; }
        public int Size { get; private set; }
        public List<MapObject> Contents { get; private set; }
        public Cell(Location loc, int size)
        {
            location = loc;
            Walls = new Wall[4];
            Neighbors = new Cell[4];
            Walls.Initialize();
            Visited = 0;
            Size = size;
            Contents = new List<MapObject>();
        }

        public override Location GetLocation() => location;

    }
}
