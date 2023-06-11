namespace MathGame
{
    public static class GameHistoryService
    {
        public static List<PlayedGame> PlayedGames { get; private set; } = new();

        public static void AddGameToHistory(
            IEnumerable<PlayedGameRecord> records,
            int correctAnswers,
            int wrongAnswers,
            string gameName,
            long totalTimeInMiliseconds)
        {
            var accuracy = correctAnswers / (double)(wrongAnswers + correctAnswers);
            PlayedGames.Add(new PlayedGame
            {
                id = PlayedGames.Count,
                totalTimeInMiliseconds = totalTimeInMiliseconds,
                records = records,
                wrongAnswers = wrongAnswers,
                gameName = gameName,
                accuracy = accuracy,
                correctAnswers = correctAnswers,
            });
        }
        public static IEnumerable<string> GetHistoryAsString() =>
            PlayedGames.Select(game =>
            $"\nId: {game.id}. You played {game.gameName}, tried to solve {game.wrongAnswers + game.correctAnswers} problems in {game.totalTimeInMiliseconds / 1000d}s.\n" +
            $"Correct answers: {game.correctAnswers}. Wrong answers: {game.wrongAnswers}. Your accuracy: {game.accuracy}\n");

        public static IEnumerable<string>? GetHistoryOfId(int id)
        {
            if (id > PlayedGames.Count - 1)
            {
                return null;
            }

            var game = PlayedGames.FirstOrDefault(game => game.id == id);

            var output = new List<string>()
            {
                $"\nId: {game.id}. You played {game.gameName}, tried to solve {game.wrongAnswers + game.correctAnswers} problems in {game.totalTimeInMiliseconds / 1000d}s.\n" +
                $"Correct answers: {game.correctAnswers}. Wrong answers: {game.wrongAnswers}. Your accuracy: {game.accuracy}\n\n",
            };

            var gameRecords = game.records.Select(x => 
                int.TryParse(x.input, out var result) && result == x.correctResult
                    ? $"{x.first} {x.operation} {x.second} = {x.input}  answered correctly in: {x.miliseconds}ms"
                    : $"{x.first} {x.operation} {x.second} = {x.input}  answered incorrectly in: {x.miliseconds}ms. Correct answer was: {x.correctResult}");
            
            output.AddRange(gameRecords);
            return output;
        }
    }
}