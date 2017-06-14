using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Maze Maze = new Maze();
            Console.WriteLine("There are " + Maze.GetCells().Length + " cells.");
            Console.ReadKey();
        }
    }
}
