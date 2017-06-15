using Newtonsoft.Json;
using System;

namespace CSharpRunner
{
    public class Game
    {
        public int turn { get; internal set; }
        internal Maze Maze;

        [JsonProperty()]
        internal Player Player;

        internal Game(Maze maze, Player player, int turn)
        {
            Maze = maze;
            Player = player;
            this.turn = turn;
        }

        /// <summary>
        /// Moves the player in the given direction
        /// </summary>
        /// <param name="dir">The direction to move the player to</param>
        public void Move(Direction dir) => Maze.Move(Player, dir);

        /// <summary>
        /// Moves the player to the given Cell
        /// </summary>
        /// <param name="c">The Cell to move to. (Must be near the current cell, and the walls must be broken)</param>
        public void Move(Cell c) => Maze.Move(Player, c);

        /// <summary>
        /// Checks if the player can move to a Cell
        /// </summary>
        /// <param name="c">The Cell to check</param>
        /// <returns>True if possible, otherwise false</returns>
        public bool IsCellReachable(Cell c) => Maze.IsCellReachable(Player, c);

        /// <summary>
        /// Gets the player's current cell
        /// </summary>
        /// <returns>The player's current cell</returns>
        public Cell GetCurrentCell() => Player.CurrentCell;

        /// <summary>
        /// Gets the start Cell of the maze
        /// </summary>
        /// <returns>The start Cell of the maze</returns>
        public Cell GetStartCell() => Maze.StartCell;

        /// <summary>
        /// Gets the end Cell of the maze, upon reaching this cell you win
        /// </summary>
        /// <returns>The end Cell of the maze</returns>
        public Cell GetEndCell() => Maze.EndCell;

        /// <summary>
        /// Returns the Cell in the given direction from the player
        /// </summary>
        /// <param name="dir"></param>
        /// <returns>The Cell which is in the given direction from the player</returns>
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

            if (Player.CurrentCell.Walls[(int)dir] == 0)
            {
                return Player.currentCell.Neighbors[(int)dir];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Outputs a message to the Console window.
        /// </summary>
        /// <param name="s">The string to output</param>
        public void Debug(string s) => Program.Log($"{{Turn {turn}}} Debug message: {s}", ConsoleColor.DarkGray);

        internal void PrepForNextTurn()
        {
            Maze.movesThisTurn = 0;
        }

        
    }
}
