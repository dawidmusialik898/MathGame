using MathGame;

try
{
    var menu = new Menu();
    menu.Run();
}
catch (NotImplementedException)
{
    Console.Write("\nChosen game was not implemented yet. Pres any key to go to Menu\n");
    Console.ReadKey();
}
catch (Exception)
{
    Console.WriteLine("\nUnexpected error occured, closing the game.\n");
}

Console.WriteLine("\nGood Bye!\n");
return;
