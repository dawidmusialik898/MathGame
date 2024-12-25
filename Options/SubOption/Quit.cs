using MathGame.MiniGames.MathGames;

namespace MathGame.Options.SubOption
{
    internal class Quit : IOption 
    {
        private bool _shouldEndProgram = false;
        public void Show()
        {
            Console.Clear();
            Console.WriteLine("Do u really want to quit? y=yes/n=no");
            var readKey = true;

            while (readKey)
            {
                var key = Console.ReadKey();

                if (key.KeyChar == 'y')
                {
                    readKey = false;
                    _shouldEndProgram = true;
                }
                else if (key.KeyChar == 'n')
                {
                    readKey = false;
                    _shouldEndProgram = false;
                }
            }
        }

        public bool ShouldEndProgram() => _shouldEndProgram; 
    }
}
