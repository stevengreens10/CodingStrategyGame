using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;

namespace CSharpRunner
{
    class Program
    {
        
        static readonly int MAX_TURNS = 1000;
        static string HTML_FILE_PATH = "\\index.html";
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
                Log("generating replay");

                GenerateHtmlFile(JsonConvert.SerializeObject(maze, Formatting.None), JsonConvert.SerializeObject(turns, Formatting.None));

                watch.Stop();
                Log($"all done");
                Process.Start(HTML_FILE_PATH);
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
            


        }

        public static void Log(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"[{watch.Elapsed.TotalSeconds}]: {message}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void GenerateHtmlFile(string mazeData, string playerData)
        {
            using (var sw = new StreamWriter(HTML_FILE_PATH))
                sw.WriteLine($"<html><head><title>Visualizer</title><style> body {{ text-align: center; }}</style><script src=\"https://cdnjs.cloudflare.com/ajax/libs/p5.js/0.5.7/p5.js\"></script><script src=\"main.js\"></script></head><body bgcolor=\"#ede24b\"><p hidden id=\"maze\" name=\"maze\">{mazeData}</p><p hidden id=\"player\" name=\"player\">{playerData}</p></body></html>");
        }

    }
}
