using MathGame.MiniGames.MathGames;

namespace MathGame.Options.SubOption
{
    internal class RandomGame : IOption
    {
        public IMathGame? Play() => throw new NotImplementedException();
        public bool ShouldEndProgram() => throw new NotImplementedException();
    }
}