﻿using System;

namespace Game
{

    class Cell
    {

        private Wall[] walls; // up right down left | 0 1 2 3 | N E S W
        private int X, Y;
        private Maze Maze;
        private int TimesVisited = 0;

        public Cell(int X, int Y, Maze Maze)
        {
            this.Maze = Maze;
            this.X = X;
            this.Y = Y;

            for(int i = 0; i < 4; i++)
            {
                walls[i] = new Wall();
                walls[i].IsBroken = (new Random().Next(100) <= 50);
            }
        }

        public Cell[] GetNeighbors()
        {
            Cell[] Neighbors = new Cell[4];
            Neighbors[0] = Maze.getCells()[X][Y + 1]; // NORTH
            Neighbors[1] = Maze.getCells()[X + 1][Y]; // EAST
            Neighbors[2] = Maze.getCells()[X][Y - 1]; // SOUTH
            Neighbors[3] = Maze.getCells()[X - 1][Y]; // WEST
            return Neighbors;
        }

    }

}