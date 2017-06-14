using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CSharpRunner
{
    class Program
    {
        static readonly string JSON_FILE_PATH = "";
        static readonly int MAX_TURNS = 1000;
        static readonly string HTML_FILE_PATH = "";

        //Args: 
        // 0: .cs file path
        // 1: rows
        // 2: cols
        // 3: cell size
        static void Main(string[] args)
        {

            var maze = new Maze(int.Parse(args[1]), int.Parse(args[2]), int.Parse(args[3]));
            var player = new Player(maze.StartCell);
            var game = new Game(maze, player);
            var turns = new List<Game>();
            var bot = Bot.FromFile(args[0]);
            var win = false;

            for (int i = 0; i < MAX_TURNS; i++)
            {
                if (win) break;

                game.PrepForNextTurn();

                game.turn = i;
                bot.DoTurn(game);

                if (game.GetCurrentCell() == game.GetEndCell())
                    win = true;

                turns.Add(game);
            }

            using (StreamWriter sw = new StreamWriter(JSON_FILE_PATH, false))
                sw.WriteLine(JsonConvert.SerializeObject(turns, Formatting.Indented));

            Process.Start(HTML_FILE_PATH);

        }
    }
}
