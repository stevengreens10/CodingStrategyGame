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
            Maze maze = new Maze();
            Console.WriteLine(maze.getCells().Length);
            Console.ReadKey();
        }
    }
}
