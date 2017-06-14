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
        public Cell CellInDirection(Direction dir)
        {
            var loc = Player.CurrentCell.GetLocation();

            //dont let the player get out of the map
            if (loc.Y == 0 && dir == Direction.North)
            {
                Program.Log("Warning: Player tried to get out of the map");
                return null;
            }
            if (loc.Y == Maze.Cells.GetLength(1) && dir == Direction.South)
            {
                Program.Log("Warning: Player tried to get out of the map");
                return null;
            }

            if (loc.X == 0 && dir == Direction.West)
            {
                Program.Log("Warning: Player tried to get out of the map");
                return null;
            }

            if (loc.X == Maze.Cells.GetLength(0) && dir == Direction.East)
            {
                Program.Log("Warning: Player tried to get out of the map");
                return null;
            }

            if (Player.CurrentCell.Walls[(int)dir])
            {
                return Player.currentCell.Neighbors[(int)dir];
            }
            else
            {
                return null;
            }
        }

        public void Debug(string s) => Program.Log($"{{Turn {GetTurn()}}} Debug message: {s}", ConsoleColor.DarkGray);

        internal void PrepForNextTurn()
        {
            Maze.movesThisTurn = 0;
        }

        
    }
}
