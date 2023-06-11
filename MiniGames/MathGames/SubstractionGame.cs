using System.Diagnostics;

using MathGame.MiniGames.SpecialGames;

namespace MathGame.MiniGames.MathGames
{
    internal class SubstractionGame : IMiniGame
    {
        private int _wrongAnswers;
        private int _correctAnswers;
        private readonly Random _rnd = new();
        private readonly List<PlayedGameRecord> _records = new();
        private long _totalTimeInMiliseconds = 0;

        public IMiniGame? Run()
        {

            Play();
            GameHistoryService.AddGameToHistory(_records, _correctAnswers, _wrongAnswers, "Subtraction", _totalTimeInMiliseconds);
            return new Menu();
        }

        private void Play()
        {
            while (true)
            {
                var clock = Stopwatch.StartNew();
                var first = _rnd.Next(0, 100);
                var second = _rnd.Next(0, 100);
                Console.Clear();
                Console.WriteLine("\nType end to end the game");
                Console.WriteLine($"\n{first} - {second}");
                var input = Console.ReadLine();
                var parsedSuccesfully = int.TryParse(input, out var result);
                if (input == "end")
                {
                    return;
                }
                else if (parsedSuccesfully && (result == (first - second)))
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
                    first = first,
                    second = second,
                    input = input,
                    miliseconds = clock.ElapsedMilliseconds,
                    operation = '-',
                    correctResult = first - second,
                });
            }
        }
    }
}