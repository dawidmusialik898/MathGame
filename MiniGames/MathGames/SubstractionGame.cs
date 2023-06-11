using System.Diagnostics;

using MathGame.MiniGames.SpecialGames;

namespace MathGame.MiniGames.MathGames
{
    internal class SubstractionGame : IMiniGame
    {
        private const string GameName = "Subtraction";
        private const char Operator = '-';
        private int _wrongAnswers;
        private int _correctAnswers;
        private readonly Random _rnd = new();
        private readonly List<PlayedGameRecord> _records = new();
        private long _totalTimeInMiliseconds = 0;

        public IMiniGame? Play()
        {

            Play();
            GameHistoryService.AddGameToHistory(_records, _correctAnswers, _wrongAnswers, GameName, _totalTimeInMiliseconds);
            return new Menu();
        }

        private void Play()
        {
            while (true)
            {
                var arguments = GetArgs();
                Console.Clear();
                Console.WriteLine("\nType end to end the game");
                Console.WriteLine($"\n{arguments.Item1} {Operator} {arguments.Item2}");
                var clock = Stopwatch.StartNew();
                var input = Console.ReadLine();
                clock.Stop();
                var parsedSuccesfully = int.TryParse(input, out var result);
                if (input == "end")
                {
                    return;
                }
                else if (parsedSuccesfully && (result == arguments.Item3))
                {
                    _correctAnswers++;
                }
                else
                {
                    _wrongAnswers++;
                }

                clock.Stop();

                _totalTimeInMiliseconds += clock.ElapsedMilliseconds;

                if (string.IsNullOrWhiteSpace(input))
                {
                    input = "[EmptyAnswer]";
                }

                _records.Add(new PlayedGameRecord()
                {
                    first = arguments.Item1,
                    second = arguments.Item2,
                    input = input,
                    miliseconds = clock.ElapsedMilliseconds,
                    operation = Operator,
                    correctResult = arguments.Item3,
                });
            }
        }

        private (int, int, int) GetArgs()
        {
            var first = _rnd.Next(0, 100);
            var second = _rnd.Next(0, 100);
            return (first, second, first-second);
        }
    }
}