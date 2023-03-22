// See https://aka.ms/new-console-template for more information
using Kata_bowling;

var game = new Game();

game.Roll(1);
game.Roll(4);

game.Roll(4);
game.Roll(5);

game.Roll(6);
game.Roll(4);

game.Roll(5);
game.Roll(5);



Console.WriteLine(game.Score());
