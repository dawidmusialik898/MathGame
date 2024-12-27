namespace MathGame.Options.MathGames
{
    public interface  IMathGame
    {
        public (int first, int second, int result, char ope) GetEquasionArguments();
        public string GameName { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public long TotalMiliseconds { get; set; }
        public List<PlayedGameRecord> Records { get; set; }
    }
}
