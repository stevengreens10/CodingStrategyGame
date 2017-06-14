using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRunner
{
    public class Game
    {
        internal int turn;
        private Maze Maze;
        private Player Player;

        internal Game(Maze maze, Player player)
        {
            Maze = maze;
            Player = player;
            turn = 0;
        }

        public void Move(Direction dir) => Maze.Move(Player, dir);
        
        public int GetTurn() => turn;

        public Cell GetCurrentCell() => Player.CurrentCell;
        public Cell GetStartCell() => Maze.StartCell;
        public Cell GetEndCell() => Maze.EndCell;


        
    }
}
