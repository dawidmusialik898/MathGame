namespace MathGame
{
    public struct PlayedGameRecord
    {
        public int first;
        public int second;
        public string? input;
        public long miliseconds;
        public char operation;
        public int correctResult;
    }

    public struct PlayedGame
    {
        public int id;
        public IEnumerable<PlayedGameRecord> records;
        public int correctAnswers;
        public int wrongAnswers;
        public string gameName;
        public long totalTimeInMiliseconds;
        public double accuracy;
    }
}