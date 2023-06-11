namespace MathGame.MiniGames
{
    public interface IMiniGame
    {
        public IMiniGame? Play();
        public char GetOperator();
        public (int first, int second, int result) GetEquasionArguments();
    }
}