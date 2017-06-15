# CodingStrategyGame
In this game, players will code a bot to attempt to solve a randomly generated maze in a set number of turns. Collaboration with shaked6540.

## Documentation
### Download
You can download the game [here](https://www.dropbox.com/s/t7ts7atfq0n4v64/CodingStrategyGame.zip?dl=0).
Once you extract the zip file, you can edit the example bot code and attempt to solve the maze with your bot.
### Running the game
To run the game, you must type the following command into a console.
```
Game.exe "[botname].cs" [cols] [rows] [cellsize]
```
`cellsize` is the side length of each cell in the maze in pixels.

### Bot Creation
Example bot: 
```csharp
using CSharpRunner;

namespace testBot1
{
    public class MyBot : IMazeBot
    {
        public void DoTurn(Game game)
        {
            if (game.GetTurn() == 0)
            {
                game.Debug("This is the first turn!");
            }
            Cell c = game.GetCurrentCell();
            for(var i = 0; i <= 3; i++){
                if(c.Walls[i])
                {
                    game.Move((Direction) i);
                    break;
                }
            }
        }
    }
}
```
This example bot loops through the directions and tries to move that direction if there is no wall in the way.

The directions map to the following numbers: 
```
North: 0
East: 1
South: 2
West: 3
```
