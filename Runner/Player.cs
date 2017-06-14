namespace Game
{
    class Player
    {

        private Cell CurrentCell;
        private Maze Maze;

        public Player(Maze Maze)
        {
            this.Maze = Maze;
            CurrentCell = Maze.GetStartingCell();
        }

        public void Move(Cell c)
        {
            Cell[] Neighbors = CurrentCell.GetNeighbors();
            bool IsNeighbor = false;

            for(int i = 0; i < Neighbors.Length; i++)
            {
                if(Neighbors[i] == c)
                {
                    IsNeighbor = true;
                }
            }

            if (IsNeighbor)
            {
                CurrentCell = c;
                CurrentCell.Visit();
            }
        }
    }
}