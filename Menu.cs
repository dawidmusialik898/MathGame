using System.Diagnostics;

using MathGame.MiniGames.MathGames;
using MathGame.Options;
using MathGame.Options.SubOption;

namespace MathGame
{
    public class Menu
    {
        public static void Run()
        {
            var shouldEndProgram = false;
            while (shouldEndProgram is false)
            {
                Show();

                var option = ChooseOption();

                if (option is IMathGame game)
                {
                    PlayGame(game);
                    GameHistoryService.AddGameToHistory(game);
                    shouldEndProgram = option.ShouldEndProgram();
                }
                else if (option is ISubOption subOption)
                {

                    shouldEndProgram = option.ShouldEndProgram();
                }
            }
        }

        private static void PlayGame(IMathGame game)
        {
            while (true)
            {
                var (first, second, correctResult, ope) = game.GetEquasionArguments();
                
                ShowContent(first, second, ope);

                var clock = Stopwatch.StartNew();
                var input = Console.ReadLine();
                clock.Stop();

                var parsedSuccesfully = int.TryParse(input, out var result);
                if (input == "end")
                {
                    return;
                }
                else if (parsedSuccesfully && (result == correctResult))
                {
                    game.CorrectAnswers++;
                }
                else
                {
                    game.WrongAnswers++;
                }
                game.TotalMiliseconds += clock.ElapsedMilliseconds;

                if (string.IsNullOrWhiteSpace(input))
                {
                    input = "[EmptyAnswer]";
                }

                game.Records.Add(new PlayedGameRecord()
                {
                    first = first,
                    second = second,
                    input = input,
                    miliseconds = clock.ElapsedMilliseconds,
                    operation = ope,
                    correctResult = correctResult,
                });
            }
        }

        private static void ShowContent(int first, int second, char ope)
        {
            Console.Clear();
            Console.WriteLine("\nType end to end the option");
            Console.WriteLine($"\n{first} {ope} {second}");
        }

        private static IOption ChooseOption()
        {
            IOption? output = null;
            while (output is null)
            {
                var key = Console.ReadKey();
                output = key.KeyChar switch
                {
                    'a' or 'A' => new AdditionGame(),
                    's' or 'S' => new SubstractionGame(),
                    'm' or 'M' => new MultiplicationGame(),
                    'd' or 'D' => new DivisionGame(),
                    'x' or 'X' => new RandomGame(),
                    'v' or 'V' => new ViewHistory(),
                    'q' or 'Q' => new Quit(),
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
