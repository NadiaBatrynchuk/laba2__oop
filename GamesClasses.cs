namespace GameClasses;

public abstract class BasicGame
{
    public abstract void GameFeature();

    public int GameRating = 0;
    public GameType Type { get; protected init; }
    public string RandomResult(string Player1, string Player2)
    {
        Random random = new Random();
        int result = random.Next(0, 100);
        if (result % 2 == 0) return Player2;
        return Player1;
    }

    public abstract void PlayGame(GameAccount Winna, GameAccount Loosa, int rating = 0);
}

// клас тренувальної гри, що наслідує базову гру. Рейтинг за цю гру рівний нулю, тому гравці не втрачають і не збільшують балів за поразку чи перемогу.
public class TrainingGame : BasicGame
{
    public TrainingGame()
    {
        Type = GameType.Training;
    }

    public override void GameFeature()
    {
        Console.WriteLine("Rating is equal to 0");
        Console.WriteLine("All players don't gain or lose rating");
    }

    // перевизначений метод симуляції гри
    public override void PlayGame(GameAccount Playa1, GameAccount Playa2, int rating = 0)
    {
        if (rating != 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rating), "Game rating must be equal to zero");
        }

        string result = this.RandomResult(Playa1.UserName, Playa2.UserName);
        if (result == Playa1.UserName)
        {
            Playa1.WinGame(Playa2.UserName, this);
            Playa2.LoseGame(Playa1.UserName, this);
        }
        else
        {
            Playa2.WinGame(Playa1.UserName, this);
            Playa1.LoseGame(Playa2.UserName, this);
        }
    }
}

// клас стандартної гри, що наслідує базову гру. Звичайна гра, в якій гравці отримують чи втрачають рейтинг на який грають.
public class StandardGame : BasicGame
{
    public StandardGame()
    {
        Type = GameType.Standard;
    }

    public override void GameFeature()
    {
        Console.WriteLine("Rating must be positive");
        Console.WriteLine("All players gain or lose rating");
    }

    // перевизначений метод симуляції гри
    public override void PlayGame(GameAccount Playa1, GameAccount Playa2, int rating)
    {
        if (rating < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rating), "Game rating mustn't be negative or equal to zero");
        }

        this.GameRating = rating;

        string result = this.RandomResult(Playa1.UserName, Playa2.UserName);
        if (result == Playa1.UserName)
        {
            Playa1.WinGame(Playa2.UserName, this);
            Playa2.LoseGame(Playa1.UserName, this);
        }
        else
        {
            Playa2.WinGame(Playa1.UserName, this);
            Playa1.LoseGame(Playa2.UserName, this);
        }
    }
}

// клас гри, в якій рейтинг може змінитися лише в одного гравця.
public class OnePlayerRatingGame : BasicGame
{
    public OnePlayerRatingGame()
    {
        Type = GameType.OnePlayerRating;
    }

    public override void GameFeature()
    {
        Console.WriteLine("Rating must be positive");
        Console.WriteLine("Only one player gain or lose rating");
    }

    // перевизначений метод симуляції гри
    public override void PlayGame(GameAccount RatingPlaya, GameAccount NonRatingPlaya, int rating)
    {
        if (rating <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rating), "Game rating mustn't be negative or equal to zero");
        }

        string result = this.RandomResult(RatingPlaya.UserName, NonRatingPlaya.UserName);
        if (result == RatingPlaya.UserName)
        {
            NonRatingPlaya.LoseGame(RatingPlaya.UserName, this);
            this.GameRating = rating;
            RatingPlaya.WinGame(NonRatingPlaya.UserName, this);

        }
        else
        {
            NonRatingPlaya.WinGame(RatingPlaya.UserName, this);
            this.GameRating = rating;
            RatingPlaya.LoseGame(NonRatingPlaya.UserName, this);

        }
    }
}

public enum GameType
{
    Standard,
    Training,
    OnePlayerRating
}