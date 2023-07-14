using System.Diagnostics;
static class GM
{

    public static int Tug {get; set;}
    public static int TugMax {get; set;}
    public static Board board {get;set;}
    public static Interface P1 {get;set;}
    public static Interface P2 {get;set;}
    public static List<Unit> UnitList;
    public static Stopwatch time;
    public static Random RNG;


    

    public static void NewGame(int seed)
    {
        UnitList = new() { Unit.Soldier(Team.Obstacle,-1,-1,-1), Unit.Bowman(Team.Obstacle,-1,-1,-1), Unit.Crossbowman(Team.Obstacle,-1,-1,-1), Unit.Halberdier(Team.Obstacle,-1,-1,-1), Unit.Scout(Team.Obstacle,-1,-1,-1), };
        Console.CursorVisible = false;
        Console.Clear();
        RNG = new Random(seed);
        board = new Board(125, 18);
        Tug = 0;
        TugMax = 10;

        P1 = new(board.boardXSize -1, UnitList.Count() - 1, ('w','s','a','d'));
        P2 = new(board.boardXSize -1, UnitList.Count() - 1, ('i','k','j','l'));

        RunGame();
    }
    static bool CheckGameEnded()
    {
        if (Tug >= TugMax || Tug <= -TugMax)
        {
            return false;
        }
        return true;
    }

    static void RunGame()
    {
        bool GameNotEnded = true;
        Console.Clear();
        time = new();
        while (GameNotEnded)
        {
            time.Restart();
            PlayerSelection();
            CheckKeyboard();
            board.UpdateAll();
            PrintEverything();
            GameNotEnded = CheckGameEnded();
            time.Stop();
            Thread.Sleep(Math.Clamp(100 - (int)time.ElapsedMilliseconds, 0, 100));
        }
        string win = Tug < 0 ? "Win" : "Lose";
        Program.Print($"\nGAME!\nYou {win}!");
    }

    static void CheckKeyboard()
    {
        P1.DoStuff();
    }
    static void PrintEverything()
    {
        Console.SetCursorPosition(0,0);
        board.PrintBoard();
        board.PrintScore();

    }

    static void PlayerSelection()
    {
        if (P1.money >= UnitList[P1.Y].Cost)
        {
            P1.Spend(UnitList[P1.Y].Cost);
            board.CreateUnit(Unit.Copy(Team.Player, P1.X, 0, UnitList[P1.Y]));
        }
        
    }
}