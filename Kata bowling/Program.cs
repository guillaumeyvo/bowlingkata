// See https://aka.ms/new-console-template for more information
using Kata_bowling;

var game = new Game();

var rolls = new List<int>() { 1, 4, 4, 5, 6, 4, 5, 5, 10, 0, 1, 7, 3, 6, 4, 10 };

foreach (var roll in rolls)
{
    game.Roll(roll);
}


Console.WriteLine(game.Score());
