using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSharpRunner
{
    public class Game
    {
        public int turn;
        internal Maze Maze;

        [JsonProperty()]
        internal Player Player;

        internal Game(Maze maze, Player player, int turn)
        {
            Maze = maze;
            Player = player;
            this.turn = turn;
        }

        public void Move(Direction dir) => Maze.Move(Player, dir);
        
        public int GetTurn() => turn;

        public Cell GetCurrentCell() => Player.CurrentCell;
        public Cell GetStartCell() => Maze.StartCell;
        public Cell GetEndCell() => Maze.EndCell;
        public void Debug(string s) => Program.Log($"{{Turn {GetTurn()}}} Debug message: {s}", ConsoleColor.DarkGray);

        internal void PrepForNextTurn()
        {
            Maze.movesThisTurn = 0;
        }

        
    }
}
