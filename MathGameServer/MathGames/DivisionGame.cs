namespace MathGame.MathGames;

public class DivisionGame : IMathGame
{
    public string GameName { get; set; } = "Division";
    public int CorrectAnswers { get; set; } = 0;
    public int WrongAnswers { get; set; } = 0;
    public long TotalMiliseconds { get; set; }
    public List<PlayedGameRecord> Records { get; set; } = new();

    public (int first, int second, int result, char ope) GetEquasionArguments()
    {
        var rnd = new Random();

        var a = rnd.Next(1, 20);
        var second = rnd.Next(1, 20);

        var first = a * second;

        return (first, second, a, '/');
    }
}
