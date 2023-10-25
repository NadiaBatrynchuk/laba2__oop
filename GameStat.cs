namespace GameClasses;

public class GameStats
{
    public readonly int GameRating; 
    public readonly string OpponentName;
    public readonly string GameResult;
    public readonly string GameType;
    public readonly int GameID;
    private static int GameIDCounter = 1; 

    public GameStats(int GameRating, string OpponentName, string GameResult, string GameType)
    {
        this.GameRating = GameRating;
        this.OpponentName = OpponentName;
        this.GameResult = GameResult;
        this.GameType = GameType;

        this.GameID = (GameIDCounter + 1) / 2;
        GameIDCounter++;
    }
}