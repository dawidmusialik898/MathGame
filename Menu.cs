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
                ShowHistory(history);       
            }

            if(key.Key == ConsoleKey.Q)
            {
                while(true)
                {
                    Console.Clear();
                    Console.WriteLine("Do you really want to quit? (Y)es/(N)o");

                    var quitResponse = Console.ReadKey();

                    if(quitResponse.Key == ConsoleKey.Y)
                    {
                        return;
                    }
                    else if(quitResponse.Key == ConsoleKey.N)
                  {
                        break;
                    }
                }
            }
        }
    }

    private static void ShowHistory(HistoryModel history)
    {
        var input = string.Empty;
        while (input != "back")
        {
            ShowGames(history); 
            input = Console.ReadLine();
            
            var inputIsValid = TryGetGameId(input, out var gameId);

            if(inputIsValid)
            {
                Console.Clear();
                var historyOfId = GameHistoryService
                    .GetHistoryOfId(gameId, history.GamesPlayed);
                
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
        }
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

    private static void ShowGames(HistoryModel history)
    {
        var games = GameHistoryService
            .GetHistoryAsString(history.GamesPlayed);

        foreach (var game in games)
        {
            Console.WriteLine(game);
        }
        
        Console.WriteLine("If you want to check game details, type 'get <id>'");
        Console.WriteLine("To go back type 'back'");
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

            var parsedSuccesfully = int.TryParse(input, out var result);
            if (input == "end")
            {
                return;
            }
            else if (parsedSuccesfully && (result == correctResult))
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
