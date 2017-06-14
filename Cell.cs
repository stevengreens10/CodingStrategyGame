using System;

namespace Game
{

    class Cell
    {

        private Wall[] walls; // up right down left | 0 1 2 3 | N E S W
        private int x, y;
        private Maze maze;
        private int timesVisited = 0;

        public Cell(int x, int y, Maze maze)
        {
            this.maze = maze;
            this.x = x;
            this.y = y;

            for(int i = 0; i < 4; i++)
            {
                walls[i] = new Wall();
                walls[i].isBroken = (new Random().Next(100) <= 50);
            }
        }

        public Cell[] getNeighbors()
        {
            Cell[] neighbors = new Cell[4];
            neighbors[0] = maze.getCells()[x][y + 1]; // NORTH
            neighbors[1] = maze.getCells()[x + 1][y]; // EAST
            neighbors[2] = maze.getCells()[x][y - 1]; // SOUTH
            neighbors[3] = maze.getCells()[x - 1][y]; // WEST
            return neighbors;
        }

    }

}