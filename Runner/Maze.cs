using System;

namespace CSharpRunner
{
    public class Maze
    {
        private Cell[,] Cells { get; set; }
        public Cell StartCell { get; }
        public Cell EndCell { get; }
        private int movesThisTurn = 0;
        internal Maze(int rows, int cols, int cellSize = 10)
        {
            Cells = new Cell[rows, cols];

            for (int x = 0; x < rows; x++)
                for (int y = 0; y < cols; y++)
                    Cells[x, y] = new Cell(new Location(x, y), cellSize);

            SetNeighbors();
            StartCell = Cells[0,0];
            EndCell = BreakWalls();
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
                    c.Neighbors[1] = loc.X == Cells.GetLength(0) ? null : Cells[x + 1, y];
                    c.Neighbors[2] = loc.Y == Cells.GetLength(1) ? null : Cells[x, y + 1];
                    c.Neighbors[3] = loc.X == 0 ? null : Cells[x - 1, y];

                }
        }
        private Cell BreakWalls() //should 'break' the walls and return the end cell
        {
            //TODO
            throw new NotImplementedException();
        }
        internal void Move(Player p, Direction dir)
        {
            if (movesThisTurn > 0) return;
            var loc = p.CurrentCell.GetLocation();

            //dont let the player get out of the map
            if (loc.Y == 0 && dir == Direction.North) return;
            if (loc.Y == Cells.GetLength(1) && dir == Direction.South) return;
            if (loc.X == 0 && dir == Direction.West) return;
            if (loc.X == Cells.GetLength(0) && dir == Direction.East) return;

            if (p.CurrentCell.Walls[(int)dir].IsBroken)
            {
                p.CurrentCell = p.CurrentCell.Neighbors[(int)dir];
                movesThisTurn++;
            }

        }
    }
}
