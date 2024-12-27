using MathGame.Options;

namespace MathGame.Options.MathGames
{
    internal class SubstractionGame : IMathGame, IOption
    {
        public string GameName { get; set; } = "Subtraction";
        public int CorrectAnswers { get; set; } = 0;
        public int WrongAnswers { get; set; } = 0;
        public long TotalMiliseconds { get; set; }
        public List<PlayedGameRecord> Records { get; set; } = new();

        public (int first, int second, int result, char ope) GetEquasionArguments()
        {
            var rnd = new Random();
            var first = rnd.Next(0, 100);
            var second = rnd.Next(0, 100);
            return (first, second, first - second, '-');
        }
        public bool ShouldEndProgram() => false;
    }
}
