namespace MathGame.MiniGames.SpecialGames
{
    internal class Quit : IMiniGame
    {
        public IMiniGame? Play()
        {
            Console.Clear();
            Console.WriteLine("Do u really want to quit? y=yes/n=no");
            IMiniGame? output = null;
            var readKey = true;

            while (readKey)
            {
                var key = Console.ReadKey();

                if (key.KeyChar == 'y')
                {
                    readKey = false;
                }
                else if(key.KeyChar == 'n')
                {
                    output = new Menu();
                    readKey = false;
                }
            }
            return output;
        }
    }
}