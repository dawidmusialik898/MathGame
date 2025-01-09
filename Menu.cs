using System.Diagnostics;
using System.Linq;

using MathGame.MathGames;

namespace MathGame;

public class Menu
{
    private readonly GameController _gameController = new GameController();

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\nV- View history games");
            Console.WriteLine("A- Addition");
            Console.WriteLine("S- Subtraction");
            Console.WriteLine("M- Multiplication");
            Console.WriteLine("D- Division");
            Console.WriteLine("Q- Quit");

            var key = Console.ReadKey();

            if(key.Key == ConsoleKey.A)
            {
                var game = _gameController.GetAdditionGame();
                PlayGame(game);
                _gameController.AddGameToHistory(game);
            }

            if(key.Key == ConsoleKey.S)
            {
                var game = _gameController.GetSubtractionGame();
                PlayGame(game);
                _gameController.AddGameToHistory(game);
            }

            if(key.Key == ConsoleKey.M)
            {
                var game = _gameController.GetMultiplicationGame();
                PlayGame(game);
                _gameController.AddGameToHistory(game);
            }

            if(key.Key == ConsoleKey.D)
            {
                var game = _gameController.GetDivisionGame();
                PlayGame(game);
                _gameController.AddGameToHistory(game);
            }

            if(key.Key == ConsoleKey.V)
            {
                var history = _gameController.GetHistory();
                ShowGamesHistory(history);       
            }

            if(key.Key == ConsoleKey.Q)
            {
                var shouldQuit = QuitDialogue();
                if (shouldQuit)
                {
                    return;
                }
            }
        }
    }

    private static bool QuitDialogue()
    {
        Console.Clear();
        Console.WriteLine("Do you really want to quit? (Y)es/(N)o");

        while(true)
        {
            var key = Console.ReadKey().Key;

            if (key == ConsoleKey.Y)
                return true;

            if (key == ConsoleKey.N)
                return false;
        }
    }

    private static void ShowGamesHistory(HistoryModel history)
    {
        while (true)
        {
            var games = GameHistoryService
                .GetHistoryAsString(history.GamesPlayed);

            foreach (var game in games)
            {
                Console.WriteLine(game);
            }
            
            Console.WriteLine("If you want to check game details, type 'get <id>'");
            Console.WriteLine("To go back type 'back'");
 
            var input = Console.ReadLine();

            if (input == "back")
                return;

            const string GetWithSpace = "get ";

            if(input.StartsWith("get ") && input.Length > GetWithSpace.Length)
            {
                ShowGameDetails(input, history);
            }
        }
    }

    private static void ShowGameDetails(string input, HistoryModel history)
    {
        var gameIdString = input.Split(' ')[1];
        var parsedSuccessfully = int.TryParse(gameIdString, out var gameId);

        Console.Clear();

        var historyOfId = parsedSuccessfully 
            ? GameHistoryService.GetHistoryOfId(gameId, history.GamesPlayed)
            : null;
        
        if (historyOfId is null)
        {
            Console.WriteLine("Game not found");
        }
        else
        {
            historyOfId.ToList().ForEach(x => Console.WriteLine(x));
        }

        Console.WriteLine("\n Press b to go back to games history");

        while (Console.ReadKey().KeyChar != 'b') {}
    }

    private static bool TryGetGameId(string? input, out int gameId)
    {
        gameId = -1;

        if (string.IsNullOrWhiteSpace(input) || !input.StartsWith("get "))
        {
            return false;
        }
        
        var getWords = input.Split(" ");
        if (getWords.Length != 2 || !int.TryParse(getWords[1], out var result))
        { 
            return false;
        }

        gameId = result;
        return true;
    }

    private static void PlayGame(IMathGame game)
    {
        while (true)
        {
            var (first, second, correctResult, ope) = game.GetEquasionArguments();
  
            Console.Clear();
            Console.WriteLine("\nType end to end the option");
            Console.WriteLine($"\n{first} {ope} {second}");

            var clock = Stopwatch.StartNew();
            var input = Console.ReadLine();
            clock.Stop();

            if (input == "end")
            {
                return;
            }
            
            var parsedSuccesfully = int.TryParse(input, out var result);
            
            if (parsedSuccesfully && (result == correctResult))
            {
                game.CorrectAnswers++;
            }
            else
            {
                game.WrongAnswers++;
            }
            game.TotalMiliseconds += clock.ElapsedMilliseconds;

            if (string.IsNullOrWhiteSpace(input))
            {
                input = "[EmptyAnswer]";
            }

            game.Records.Add(new PlayedGameRecord()
            {
                first = first,
                second = second,
                input = input,
                miliseconds = clock.ElapsedMilliseconds,
                operation = ope,
                correctResult = correctResult,
            });
        }
    }
}
