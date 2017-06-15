using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;
using System.Linq;

namespace CSharpRunner
{
    class Program
    {
        
        static readonly int MAX_TURNS = 100000;
        static string HTML_FILE_PATH = Directory.GetCurrentDirectory() + "\\index.html";
        static Stopwatch watch;
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
            watch.Start();

            try
            {
                var bot = Bot.FromFile(args[0]);
                var maze = new Maze(int.Parse(args[1]), int.Parse(args[2]), int.Parse(args[3]));
                var lastPlayer = new Player(maze.StartCell);
                var turns = new List<Game>();
                var win = false;
                Game game;
                Player player;

                Log("Initiallizing bot..");
                bot.Init();
                Log("Bot initiallized");
                System.Threading.Thread.Sleep(1000);

                for (int i = 0; i < MAX_TURNS; i++)
                {
                    if (win) break;
                    player = new Player(lastPlayer.currentCell);
                    game = new Game(maze, player, i);
                    game.PrepForNextTurn();

                    bot.DoTurn(game);

                    if (game.GetCurrentCell() == game.GetEndCell())
                        win = true;

                    turns.Add(game);
                    lastPlayer = player;

                }

                Log("bot finished running");
                Log("generating replay");

                WriteIntoHtmlFile(JsonConvert.SerializeObject(maze, Formatting.None), JsonConvert.SerializeObject(turns, Formatting.None));

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

        public static void WriteIntoHtmlFile(string mazeData, string playerData)
        {
            File.WriteAllText(HTML_FILE_PATH, File.ReadAllText(TEMPLATE_FILE_PATH).Replace("##MAZE CODE##", mazeData).Replace("##PLAYER CODE##", playerData));
        }

    }
}
