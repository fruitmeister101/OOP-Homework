static class GM
{

    public static int Tug {get; set;}
    static public Board board {get;set;}
    
    public static Random RNG;


    

    public static void NewGame(int seed)
    {
        Console.Clear();
        RNG = new Random(seed);
        board = new Board(125, 18);
        Tug = 0;
        board.PrintBoard();
        RunGame();
    }
    static void CheckGameEnded()
    {

    }

    static void RunGame()
    {
        bool GameNotEnded = true;
        while (GameNotEnded)
        {
            board.UpdateAll();
            GameNotEnded = false;
        }
    }

}