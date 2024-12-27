namespace MathGame;

internal class Quit
{
    public bool OpenQuitDialogue()
    {
        Console.Clear();
        Console.WriteLine("Do u really want to quit? y=yes/n=no");

        while (true)
        {
            var key = Console.ReadKey();

            if (key.KeyChar == 'y')
            {
                return true;
            }
            else if (key.KeyChar == 'n')
            {
                return false;
            }
        }
    }
}
