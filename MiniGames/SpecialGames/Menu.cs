using MathGame.MiniGames.MathGames;

namespace MathGame.MiniGames.SpecialGames
{
    public class Menu
    {
        public void Run()
        {
            Show();

            var game = ChooseOption();

            var (first, second, result) = game.GetEquasionArguments();
            var ope = game.GetOperator();

            Console.Clear();
            Console.WriteLine("\nType end to end the game");
            Console.WriteLine($"\n{first} {ope} {second}");

        }

        private static IMiniGame ChooseOption()
        {
            IMiniGame? output= null;
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
