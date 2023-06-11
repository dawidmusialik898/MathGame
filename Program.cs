// See https://aka.ms/new-console-template for more information

using MathGame;

do
{
    try
    {
        Menu.Run();
    }
    catch (NotImplementedException)
    {
        Console.Write("\nChosen game was not implemented yet. Pres any key to go to Menu\n");
        Console.ReadKey();
    }
    catch (Exception)
    {
        Console.WriteLine("\nUnexpected error occured, closing the game.\n");
        break;
    }
} while (true);

Console.WriteLine("\nGood Bye!\n");
return;