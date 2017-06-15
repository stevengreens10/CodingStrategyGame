using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CSharpRunner
{
    public class Maze
    {
        [JsonProperty()]
        internal Cell[,] Cells { get; set; }

        [JsonProperty()]
        public Cell StartCell { get; }

        [JsonProperty()]
        public Cell EndCell { get; }

        internal int movesThisTurn = 0;

        internal Maze(int rows, int cols, int cellSize = 10)
        {
            Cells = new Cell[rows, cols];

            Program.Log($"Generating maze [{rows} x {cols}]..");

            for (int x = 0; x < rows; x++)
                for (int y = 0; y < cols; y++)
                    Cells[x, y] = new Cell(new Location(x, y), cellSize);

            SetNeighbors();
            StartCell = Cells[0,0];
            EndCell = BreakWalls();

            Program.Log("Finished generating maze");

        }

        private void SetNeighbors()
        {
            Location loc;
            Cell c;
            for (int x = 0; x < Cells.GetLength(0); x++)
                for (int y = 0; y < Cells.GetLength(1); y++)
                {
                    c = Cells[x, y];
                    loc = c.GetLocation();

                    c.Neighbors[0] = loc.Y == 0 ? null : Cells[x, y - 1];
                    c.Neighbors[1] = loc.X == Cells.GetLength(0) - 1 ? null : Cells[x + 1, y];
                    c.Neighbors[2] = loc.Y == Cells.GetLength(1) - 1 ? null : Cells[x, y + 1];
                    c.Neighbors[3] = loc.X == 0 ? null : Cells[x - 1, y];

                }
        }
        private Cell BreakWalls() //should 'break' the walls and return the end cell
        {
            //TODO
            Cell current = Cells[0, 0];
            Stack<Cell> stack = new Stack<Cell>();
            int cells_left = Cells.GetLength(0) * Cells.GetLength(1);
            while (cells_left > 0 || stack.Count != 0)
            {
                cells_left = 0;
                current.Visited++;
                var next = current.Neighbors.Where(x => x != null && x.Visited == 0).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                if (next != null)
                {
                    stack.Push(current);
                    next.Visited++;
                    RemoveWalls(current, next);
                    current = next;
                }
                else if (stack.Count != 0)
                {
                    current = stack.Pop();

                }

                for (int i = 0; i < Cells.GetLength(0); i++)
                    for (int j = 0; j < Cells.GetLength(1); j++)
                        if (Cells[i, j].Visited == 0) cells_left++;

            }

            for (int i = 0; i < Cells.GetLength(0); i++)
                for (int j = 0; j < Cells.GetLength(1); j++)
                    Cells[i, j].Visited = 0;

            return Cells[Cells.GetLength(0) - 1, Cells.GetLength(1) - 1];

        }
        private void RemoveWalls(Cell c, Cell c2)
        {
            if (c.GetLocation().X == c2.GetLocation().X) // need to remove up or down
            {
                if (c.GetLocation().Y > c2.GetLocation().Y)
                {
                    c.Walls[0] = true;
                    c2.Walls[2] = true;
                }

                else
                {
                    c.Walls[2] = true;
                    c2.Walls[0] = true;
                }
            }
            else if (c.GetLocation().Y == c2.GetLocation().Y) // need to remove left or right
            {
                if (c.GetLocation().X > c2.GetLocation().X)
                {
                    c.Walls[3] = true;
                    c2.Walls[1] = true;
                }
                else
                {
                    c.Walls[1] = true;
                    c2.Walls[3] = true;
                }
            }
        }
        internal void Move(Player p, Direction dir)
        {
            if (movesThisTurn > 0)
            {
                Program.Log("Warning: Player tried to move more than once");
                return;
            }
            
            var loc = p.CurrentCell.GetLocation();

            //dont let the player get out of the map
            if (loc.Y == 0 && dir == Direction.North)
            {
                Program.Log("Warning: Player tried to get out of the map");
                return;
            }
            if (loc.Y == Cells.GetLength(1) && dir == Direction.South)
            {
                Program.Log("Warning: Player tried to get out of the map");
                return;
            }

            if (loc.X == 0 && dir == Direction.West)
            {
                Program.Log("Warning: Player tried to get out of the map");
                return;
            }

            if (loc.X == Cells.GetLength(0) && dir == Direction.East)
            {
                Program.Log("Warning: Player tried to get out of the map");
                return;
            }

            if (p.CurrentCell.Walls[(int)dir])
            {
                p.currentCell = p.currentCell.Neighbors[(int)dir];
                movesThisTurn++;
            }
            else
            {
                Program.Log($"player tried to get into a wall");
            }

        }
    }
}
