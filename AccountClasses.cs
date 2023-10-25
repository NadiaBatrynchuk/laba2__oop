namespace GameClasses;

public class GameAccount
{
    public string UserName { get; set; }
    public int BaseRating { get; set; }
    public int CurrentRating
    {
        get
        {
            /*int val = 0;
            foreach (var elem in allGameStats)
            {
                val += elem.GameRating;
            }
            return (val + BaseRating);*/

            int val = 0;
            foreach (var elem in allGameStats)
            {
                val += elem.GameRating;
            }
            if (val + BaseRating > 1)
                return val + BaseRating;
            return 1;

        }
        /*set
        {
            CurrentRating = value;
        }*/
    }
    public int GamesCount { get; set; }
    public List<GameStats> allGameStats = new List<GameStats>();

    public GameAccount(string UserName, int BaseRating, int GamesCount)
    {
        this.UserName = UserName;
        this.BaseRating = Math.Max(1, BaseRating);
        this.GamesCount = GamesCount;
    }

    public virtual void WinGame(string OpponentName, BasicGame game)
    {
        var currentGame = new GameStats(game.GameRating, OpponentName, "Win", game.Type.ToString());
        allGameStats.Add(currentGame);
        this.GamesCount++;
    }

    public virtual void LoseGame(string OpponentName, BasicGame game)
    {
        var currentGame = new GameStats(-game.GameRating, OpponentName, "Lose", game.Type.ToString());
        allGameStats.Add(currentGame);
        this.GamesCount++;
    }

    public void GetStats()
    {
        Console.WriteLine($"\n--- {this.UserName}'s account game history:\n");
        Console.WriteLine("////////////////////");
        foreach (var elem in allGameStats)
        {
            Console.WriteLine($"\nOpponent Name: {elem.OpponentName}");
            Console.WriteLine($"Game Result: {elem.GameResult}");
            Console.WriteLine($"Game Rating: {elem.GameRating}");
            Console.WriteLine($"Game Type: {elem.GameType}");
            Console.WriteLine($"Game ID: {elem.GameID}\n");
            Console.WriteLine("////////////////////");
        }
    }
}

// клас Тренувального акаунту, на якому не змінюється рейтинг за програш чи поразку
public class TrainingAccount : GameAccount
{
    public TrainingAccount(string UserName, int CurrentRating, int GamesCount) : base(UserName, CurrentRating, GamesCount) {}

    // перевизначений метод перемоги в грі
    public override void WinGame(string OpponentName, BasicGame game)
    {
        var currentGame = new GameStats(0, OpponentName, "Win", game.Type.ToString());
        allGameStats.Add(currentGame);
        this.GamesCount++;
    }

    // перевизначений метод програшу в грі
    public override void LoseGame(string OpponentName, BasicGame game)
    {
        var currentGame = new GameStats(0, OpponentName, "Lose", game.Type.ToString());
        allGameStats.Add(currentGame);
        this.GamesCount++;
    }
}

// клас Преміум акаунту, на якому знімається вдвічі менше та нараховується в 1.5 разів більше балів.
public class PremiumAccount : GameAccount
{
    public PremiumAccount(string UserName, int CurrentRating, int GamesCount) : base(UserName, CurrentRating, GamesCount) {}

    // перевизначений метод перемоги в грі
    public override void WinGame(string OpponentName, BasicGame game)
    {
        var currentGame = new GameStats(game.GameRating * 3 / 2, OpponentName, "Win", game.Type.ToString());
        allGameStats.Add(currentGame);
        this.GamesCount++;
    }

    // перевизначений метод програшу в грі
    public override void LoseGame(string OpponentName, BasicGame game)
    {
        var currentGame = new GameStats(game.GameRating / 2, OpponentName, "Lose", game.Type.ToString());
        allGameStats.Add(currentGame);
        this.GamesCount++;
    }
}