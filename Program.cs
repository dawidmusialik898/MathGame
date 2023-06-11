// See https://aka.ms/new-console-template for more information
using MathGame.MiniGames;
using MathGame.MiniGames.SpecialGames;

var game = new Menu();

do
{
    try
    {
        game = game.Run();
    }
    catch (NotImplementedException)
    {
        Console.Write("\nChosen game was not implemented yet. Pres any key to go to Menu\n");
        Console.ReadKey();
        game = new Menu();
    }
    catch (Exception)
    {
        Console.WriteLine("\nUnexpected error occured, closing the game.\n");
        game = null;
    }
} while (game != null);

Console.WriteLine("\nGood Bye!\n");
return;