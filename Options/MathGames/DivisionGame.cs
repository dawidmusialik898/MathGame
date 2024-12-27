using MathGame.Options;

namespace MathGame.Options.MathGames
{
    internal class DivisionGame : IMathGame, IOption
    {
        public string GameName { get; set; } = "Division";
        public int CorrectAnswers { get; set; } = 0;
        public int WrongAnswers { get; set; } = 0;
        public long TotalMiliseconds { get; set; }
        public List<PlayedGameRecord> Records { get; set; } = new();

        public (int first, int second, int result, char ope) GetEquasionArguments()
        {
            var rnd = new Random();

            var a = rnd.Next(0, 10);
            var second = rnd.Next(0, 10);

            var first = a * second;

            return (first, second, a, '/');
        }
        public bool ShouldEndProgram() => false;
    }
}
