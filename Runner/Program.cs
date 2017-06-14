using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;

namespace CSharpRunner
{
    class Program
    {
        static string MAZE_FILE_PATH = "";
        static string PLAYER_MOVEMENT_FILE_PATH = "";
        
        static readonly int MAX_TURNS = 1000;
        static string HTML_FILE_PATH = "";
        static Stopwatch watch;

        //Args: 
        // 0: .cs file path
        // 1: rows
        // 2: cols
        // 3: cell size
        static void Main(string[] args)
        {
            Console.WriteLine("Starting.. ");
            watch = new Stopwatch();
            watch.Start();

            #if DEBUG

            PLAYER_MOVEMENT_FILE_PATH = Directory.GetCurrentDirectory() + "\\playerdata.json";
            MAZE_FILE_PATH = Directory.GetCurrentDirectory() + "\\mazedata.json";
            HTML_FILE_PATH = Directory.GetCurrentDirectory() + "\\index.html";

            #endif

            try
            {

                var maze = new Maze(int.Parse(args[1]), int.Parse(args[2]), int.Parse(args[3]));
                var player = new Player(maze.StartCell);
                var turns = new List<Game>();
                var bot = Bot.FromFile(args[0]);
                var win = false;
                Game game;

                for (int i = 0; i < MAX_TURNS; i++)
                {
                    if (win) break;
                    game = new Game(maze, player, i);
                    game.PrepForNextTurn();

                    bot.DoTurn(game);

                    if (game.GetCurrentCell() == game.GetEndCell())
                        win = true;

                    turns.Add(game);
                }

                Log("bot finished running");
                Log("writing JSON data");

                using (var sw = new StreamWriter(MAZE_FILE_PATH, false))
                    sw.WriteLine(JsonConvert.SerializeObject(maze, Formatting.None));

                using (var sw = new StreamWriter(PLAYER_MOVEMENT_FILE_PATH, false))
                    sw.WriteLine(JsonConvert.SerializeObject(turns, Formatting.None));

                Log("finished writing JSON data");

                watch.Stop();
                Log($"all done");
                Console.ReadLine();
            }
            catch (InvalidOperationException ioe)
            {
                Log($"[CompileError]: {ioe.Message}", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                Log($"Exception {ex.Message}");
            }
            finally
            {
                Console.ReadLine();
            }
            
            #if !DEBUG
            Process.Start(HTML_FILE_PATH);
            #endif

        }

        public static void Log(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"[{watch.Elapsed.TotalSeconds}]: {message}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
