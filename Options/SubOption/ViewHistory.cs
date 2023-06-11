using MathGame.MiniGames.MathGames;

namespace MathGame.Options.SubOption
{
    internal class ViewHistory : IOption
    {
        public void Play()
        {

            var input = "";
            while (input != "back")
            {
                ShowGamesHistory();

                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && input.StartsWith("get "))
                {
                    ShowGameDetails(input);
                }
            }
        }

        private static void ShowGameDetails(string? input)
        {
            var getWords = input.Split(" ");
            if (getWords.Length == 2 && int.TryParse(getWords[1], out var result))
            {
                Console.Clear();
                var historyOfId = GameHistoryService.GetHistoryOfId(result);
                if (historyOfId is null)
                    Console.WriteLine("Game not found");
                else
                    foreach (var item in historyOfId)
                    {
                        Console.WriteLine(item);
                    }

                Console.WriteLine("\n Press b to go back to games history");

                while (Console.ReadKey().KeyChar != 'b')
                    ;
            }
        }

        private static void ShowGamesHistory()
        {
            Console.Clear();
            var history = GameHistoryService.GetHistoryAsString();
            foreach (var item in history)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Write 'get <id>' to get game details");
            Console.WriteLine("Write back to go back to menu");
        }

        public bool ShouldEndProgram() => throw new NotImplementedException();
    }
}