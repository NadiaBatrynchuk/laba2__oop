using GameClasses;

class lab2
{
    static void Main(string[] args)
    {
        var Player1 = new GameAccount("Vasya", 900, 4);
        var Player2 = new GameAccount("Petya", -450, 2);
        var Player3 = new PremiumAccount("Kolya", 200, 3);
        var Player4 = new TrainingAccount("Nikita", 40, 1);


        GameFactory factory = new();
        var game1 = factory.CreateGame(GameType.Training);
        var game2 = factory.CreateGame(GameType.Standard);
        var game3 = factory.CreateGame(GameType.OnePlayerRating);

        try
        {
            game1.PlayGame(Player1, Player2);
            game2.PlayGame(Player1, Player3, 350);
            game3.PlayGame(Player2, Player3, 130);

            game2.PlayGame(Player4, Player3, 260);
        }
        catch(ArgumentOutOfRangeException e)
        {
            Console.WriteLine("Exception caught creating a game with negative rating");
            Console.WriteLine(e.ToString());
            return;
        }

        Player1.GetStats();
        Console.WriteLine(Player1.CurrentRating);
        Player2.GetStats();
        Console.WriteLine(Player2.CurrentRating);
        Player3.GetStats();
        Console.WriteLine(Player3.CurrentRating);
        Player4.GetStats();
        Console.WriteLine(Player4.CurrentRating);
    }
}
    