namespace GameClasses;

public class GameFactory
{
    public BasicGame CreateGame(GameType type)
    {
        if (type == GameType.Standard)
        {
            return new StandardGame();
        }
        else if (type == GameType.Training)
        {
            return new TrainingGame();
        }
        else if (type == GameType.OnePlayerRating)
        {
            return new OnePlayerRatingGame();
        }
        else
        {
            throw new ArgumentException("Непідтримуваний тип гри");
        }
    }
}