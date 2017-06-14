namespace Game
{
    class Maze
    {

        private Cell[][] cells;
        private Cell startCell;
        private Cell endCell;
        private Player player;

        public Maze()
        {
            for (var x = 0; x < 10; x++)
            {
                for(var y = 0; y < 10; y++)
                {
                    cells[x][y] = new Cell(x,y,this);
                }
            }
            startCell = cells[0][0];
            endCell = cells[9][9];
            player = new Player();
        }

        public Cell[][] getCells()
        {
            return cells;
        }
    }
}