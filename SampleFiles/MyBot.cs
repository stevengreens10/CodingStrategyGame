using CSharpRunner;
using System.Collections.Generic;
using System.Linq;

namespace testBot1
{
    public class MyBot : IMazeBot
    {
        static List<Cell> history;
        public void DoTurn(Game game)
        {
            if (game.GetTurn() == 0)
            {
                history = new List<Cell>();
            }

            Cell c = game.GetCurrentCell();

            if (!history.Contains(c)) history.Add(c);
            if (!c.Walls.Where(x => x).Any())
            {
                game.Debug("Im stuck! no walls are open");
                game.Debug("Cell {c.GetLocation().X} {c.GetLocation().Y}");
                return;
            }

            for (int i = 0; i < 3; i++)
                if (c.Walls[i])
                    if (!history.Contains(game.CellInDirection((Direction)i)))
                        game.Move((Direction)i);

            for (int i = 0; i < 3; i++)
                if (c.Walls[i])
                    game.Move((Direction)i);

        }
    }
}
