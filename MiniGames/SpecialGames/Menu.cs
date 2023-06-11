using MathGame.MiniGames.MathGames;

namespace MathGame.MiniGames.SpecialGames
{
    public class Menu : IMiniGame
    {
        public IMiniGame? Run()
        {
            Show();

            IMiniGame? output = null;

            while (output is null)
            {
                var key = Console.ReadKey();
                output = key.KeyChar switch
                {
                    'a' => new AdditionGame(),
                    's' => new SubstractionGame(),
                    'm' => new MultiplicationGame(),
                    'd' => new DivisionGame(),
                    'x' => new RandomGame(),
                    'v' => new ViewHistory(),
                    'q' => new Quit(),
                    _ => null,
                };
            }
            return output;
        }

        private static void Show()
        {
            Console.Clear();
            Console.WriteLine("\nV- View history games");
            Console.WriteLine("A- Addition");
            Console.WriteLine("S- Subtraction");
            Console.WriteLine("M- Multiplication");
            Console.WriteLine("D- Division");
            Console.WriteLine("X- Random choice");
            Console.WriteLine("Q- Quit");
        }
    }
}
