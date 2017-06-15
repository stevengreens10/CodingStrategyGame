using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CSharpRunner
{
    class Program
    {
        
        static readonly int MAX_TURNS = 100000;
        static readonly string HTML_FILE_PATH = Directory.GetCurrentDirectory() + "\\index.html";
        static Stopwatch watch, botRuntime;
        static readonly string TEMPLATE_FILE_PATH = Directory.GetCurrentDirectory() + "\\Template.html";

        //Args: 
        // 0: .cs file path
        // 1: rows
        // 2: cols
        // 3: cell size
        static void Main(string[] args)
        {
            Console.WriteLine("Starting.. ");
            watch = new Stopwatch();
            botRuntime = new Stopwatch();
            watch.Start();
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            try
            {
                var bot = Bot.FromFile(args[0]);
                var maze = new Maze(int.Parse(args[1]), int.Parse(args[2]), int.Parse(args[3]));
                var lastPlayer = new Player(maze.StartCell);
                var stats = new { TurnsCount = 0, Win = false };
                Game game;
                Player player;
                botRuntime.Start();
                Log("Initiallizing bot..");
                bot.Init();
                Log("Bot initiallized");

                for (int i = 0; i < MAX_TURNS; i++)
                {
                    if (stats.Win) break;
                    player = new Player(lastPlayer.currentCell);
                    game = new Game(maze, player, i);
                    game.PrepForNextTurn();

                    bot.DoTurn(game);

                    sb.Append(JsonConvert.SerializeObject(game));
                    sb.Append(",");

                    if (game.GetCurrentCell() == game.GetEndCell())
                        stats = new { TurnsCount = i, Win = true };

                    lastPlayer = player;

                }
                botRuntime.Stop();
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]");

                Log("bot finished running");
                Log("generating replay");

                WriteIntoHtmlFile(JsonConvert.SerializeObject(maze, Formatting.None), sb.ToString());

                watch.Stop();
                Log($"all done");

                Log($"Total turns: {stats.TurnsCount}, Bot run time: {botRuntime.Elapsed.TotalSeconds} seconds\nPress Enter to load replay");
                Console.ReadLine();

                Process.Start(HTML_FILE_PATH);
                
            }
            catch (InvalidOperationException ioe)
            {
                Log($"[CompileError]: {ioe.Message}", ConsoleColor.Red);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Log($"Exception {ex.Message}");
                Console.ReadLine();
            }
        }

        public static void Log(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"[{watch.Elapsed.TotalSeconds}]: {message}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void WriteIntoHtmlFile(string mazeData, string playerData)
        {
            File.WriteAllText(HTML_FILE_PATH, File.ReadAllText(TEMPLATE_FILE_PATH).Replace("##MAZE CODE##", mazeData).Replace("##PLAYER CODE##", playerData));
        }

    }
}
