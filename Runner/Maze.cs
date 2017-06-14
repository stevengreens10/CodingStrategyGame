namespace Game
{
    class Maze
    {

        private Cell[,] Cells;
        private Cell StartCell;
        private Cell EndCell;
        private Player Player;

        public Maze()
        {
            Cells = new Cell[10,10];
            for (int x = 0; x < 10; x++)
            {
                for(int y = 0; y < 10; y++)
                {
                    Cells[x,y] = new Cell(x,y,this);
                }
            }
            StartCell = Cells[0,0];
            EndCell = Cells[9,9];
            Player = new Player(this);
        }

        public Cell[,] GetCells()
        {
            return Cells;
        }

        public Cell GetStartingCell()
        {
            return StartCell;
        }
    }
}