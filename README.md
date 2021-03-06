# CodingStrategyGame
In this game, players will code a bot to attempt to solve a randomly generated maze in a set number of turns. Created by stevengreens10 and shaked6540

## Documentation
### Download
You can download the game [here](https://mega.nz/#!0ghm3TDQ!fk0iFgCTSFZGgvD_2y0e9BhqP4tx2uTuL4dFvLXR3CQ).
Once you extract the rar file, you can edit the example bot code and attempt to solve the maze with your bot.
### Running the game
To run the game, you must type the following command into a console.
```
CSharpRunner.exe "[botname].cs" [cols] [rows] [cellsize]
```
`cellsize` is the side length of each cell in the maze in pixels.

Or, simply click on "Run.bat". It will load the default "MyBot.cs" file

### Bot Creation
Example bot: 
```csharp
using CSharpRunner;

namespace testBot1
{
    public class MyBot : IMazeBot
    {
        public void Init()
        {
            //Init all your variables here.
            //Note: You cant get the Game object here, this method runs only once before the first turn
        }
        
        public void DoTurn(Game game)
        {
            //Write all your code here, this method will run every turn
        }
    }
}
```
This example bot loops through the directions and tries to move that direction if there is no wall in the way.

### API
`public void Init(){}` This is a function you must include for your bot to work. The code in this function will be executed once, before it begins starting its turns.

`public void DoTurn(Game game){}` This is a function you must include for your bot to work. The code in this function will be called once each turn.

`game.GetTurn()` returns the current turn number.

`game.Debug(String)` will display a debug message in the console along with the current turn number.

`game.GetCurrentCell()` returns the cell that the player is currently at.

`game.GetStartCell()` returns the cell that the player starts at.

`game.GetEndCell()` returns the cell at the end of the maze.

`game.IsCellReachable(Cell)` returns true if the player can move into the given cell.

`game.Move(Direction)` will move the player in the specified direction. If there is a wall in the way, a warning message will appear in the console. Note that you may only move once each turn. returns true if the player moved.

The directions map to the following numbers: 
```
North: 0
East: 1
South: 2
West: 3
```

`game.Move(Cell)` will move the player to a certain cell if possible. If the player can not move to the given cell, a warning message will appear in the console. returns true if the player moved.

`cellObj.WallsArray[dirNumber]` yields a boolean that dictates whether a wall is present or not in that direction. `true` means that there is a wall.

`cellObj.GetLocation()` returns a Location object that has the column and row coordinate of the cell. These are stored in `location.x` and `location.y` respectively.

`game.CellInDirection(Direction)` returns a cell in the specified direction from the current cell. If that direction is somewhere outside of the maze, the function will return null, and a warning message will appear in the console


