namespace Game
{
    class Player
    {

        private Cell currentCell;

        public Player(Maze maze)
        {

        }

        public void Move(Cell c)
        {
            Cell[] neighbors = currentCell.getNeighbors();
            bool isNeighbor = false;

            for(int i = 0; i < neighbors.Length; i++)
            {
                if(neighbors[i] == c)
                {
                    isNeighbor = true;
                }
            }

            if (isNeighbor)
            {
                currentCell = c;
            }
        }
    }
}