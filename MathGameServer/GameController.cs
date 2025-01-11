using MathGame.MathGames;

namespace MathGame;

public class GameController 
{
    // TODO: now it's in memory in future create better persistance and inject here some datacontext/repository
    private readonly HistoryModel _historyInMemory = new HistoryModel();
    
    // TODO: mb instead of method per game just return IMathGame based on paramenter
    // then concrete games could be internal.
    // probably the best would be to create and inject here gamefactory using parameters returning IMath
    public AdditionGame GetAdditionGame()
    {
        return new ();
    }
   
    public SubtractionGame GetSubtractionGame()
    {
        return new ();
    }
   
    public MultiplicationGame GetMultiplicationGame()
    {
        return new();
    }
    
    public DivisionGame GetDivisionGame()
    {
        return new();
    }

    public HistoryModel GetHistory()
    {
        return _historyInMemory;
    }

    public void AddGameToHistory(IMathGame game)
    {
        _historyInMemory.GamesPlayed.Add(new PlayedGame
        {
            accuracy = (double)game.CorrectAnswers / 
            (double)(game.CorrectAnswers + game.WrongAnswers),
            correctAnswers = game.CorrectAnswers,
            wrongAnswers = game.WrongAnswers,
            gameName = game.GameName,
            id = _historyInMemory.GamesPlayed.Count,
            records = game.Records,
            totalTimeInMiliseconds = game.TotalMiliseconds,
        });
    }
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

public class HistoryModel // TODO: move to separate file
{
    public readonly List<PlayedGame> GamesPlayed = new();
}
