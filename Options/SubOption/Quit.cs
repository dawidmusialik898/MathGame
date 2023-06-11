using MathGame.MiniGames.MathGames;

namespace MathGame.Options.SubOption
{
    internal class Quit : IOption, ISubOption
    {
        public void Show()
        {
            Console.Clear();
            Console.WriteLine("Do u really want to quit? y=yes/n=no");
            IMathGame? output = null;
            var readKey = true;

            while (readKey)
            {
                var key = Console.ReadKey();

                if (key.KeyChar == 'y')
                {
                    readKey = false;
                }
                else if (key.KeyChar == 'n')
                {
                    //output = new Menu();
                    readKey = false;
                }
            }
            //return output;
        }

        public bool ShouldEndProgram() => throw new NotImplementedException();
    }
}