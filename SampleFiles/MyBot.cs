using CSharpRunner;

namespace testBot1
{
    public class MyBot : IMazeBot
    {
        public void DoTurn(Game game)
        {
            try
            {
                if (game.GetTurn() == 0)
                {
                    game.Debug("This is the first turn!");
                }
                Cell c = game.GetCurrentCell();
                for (int i = 0; i < 3; i++)
                {
                    if (c.Walls[i])
                    {
                        game.Move((Direction)i);
                        break;
                    }
                }
            }
            catch
            {
                
            }
        }
    }
}
