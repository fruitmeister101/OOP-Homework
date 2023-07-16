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
        Console.Clear();
        Program.Print("Rules\n");
        Thread.Sleep(0750);
        Program.Print("Get Units across the enemy line to score\n");
        Thread.Sleep(0750);
        Program.Print("Scoring works like a tug-o-war, you must score more than your opponent to make progress\n");
        Thread.Sleep(0750);
        Program.Print("Units move and attack on their own\n");
        Thread.Sleep(0750);
        Program.Print("W and S change what lane units are sent on\n");
        Thread.Sleep(0750);
        Program.Print("A and D change what Units are sent\n");
        Thread.Sleep(0750);
        Program.Print("You are the Left Army, your opponent - the Right\n");
        Thread.Sleep(0750);
        Program.Print("Press Any Key when Ready");
        while (!Console.KeyAvailable)
        {
        }
        


        UnitList = new() { Unit.Soldier(Team.Obstacle,-1,-1,-1), Unit.Bowman(Team.Obstacle,-1,-1,-1), Unit.Crossbowman(Team.Obstacle,-1,-1,-1), Unit.Halberdier(Team.Obstacle,-1,-1,-1), Unit.Scout(Team.Obstacle,-1,-1,-1), Unit.Tank(Team.Obstacle, -1,-1,-1), Unit.Ninja(Team.Obstacle,-1,-1,-1), Unit.Summoner(Team.Obstacle,-1,-1,-1)};
        Console.CursorVisible = false;
        Console.Clear();
        RNG = new Random(seed);
        board = new Board(125, 18);
        Tug = 0;
        TugMax = 10;
        /*
        board.CreateUnit(Unit.Soldier(Team.Player, 3, 10));
        board.CreateUnit(Unit.Soldier(Team.Player, 4, 10));
        board.CreateUnit(Unit.Soldier(Team.Player, 5, 10));
        board.CreateUnit(Unit.Soldier(Team.Player, 6, 10));
        board.CreateUnit(Unit.Soldier(Team.Player, 7, 10));
        board.CreateUnit(Unit.Soldier(Team.Player, 8, 10));
        board.CreateUnit(Unit.Soldier(Team.Player, 9, 10));
        board.CreateUnit(Unit.Soldier(Team.Player, 10, 10));

        board.CreateUnit(Unit.Halberdier(Team.Enemy, 3, 40));
        board.CreateUnit(Unit.Halberdier(Team.Enemy, 4, 40));
        board.CreateUnit(Unit.Halberdier(Team.Enemy, 5, 40));
        board.CreateUnit(Unit.Halberdier(Team.Enemy, 6, 40));
        board.CreateUnit(Unit.Halberdier(Team.Enemy, 7, 40));
        board.CreateUnit(Unit.Halberdier(Team.Enemy, 8, 40));
        board.CreateUnit(Unit.Halberdier(Team.Enemy, 9, 40));
        board.CreateUnit(Unit.Halberdier(Team.Enemy, 10, 40));
        */
        P1 = new(board._boardXSize -1, UnitList.Count() - 1, ('w','s','a','d'));
        P2 = new(board._boardXSize -1, UnitList.Count() - 1, ('i','k','j','l'), true);

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
        time = new();
        while (GameNotEnded) // Main Loop!!
        {
            time.Restart();
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
        PlayerSelection(P1, Team.Player);
        PlayerSelection(P2, Team.Enemy);
    }
    static void PrintEverything()
    {
        Console.SetCursorPosition(0,0);
        Program.Print(board.PrintBoard() + board.PrintScore() + board.PrintSelection() + board.PrintKeys());

    }
    static void PlayerSelection(Interface p, Team t)
    {
        p.DoStuff();
        if (!p._auto)
        {
            if (p._money >= UnitList[p._Y]._cost)
            {
                p.Spend(UnitList[p._Y]._cost);
                var temp = UnitList[p._Y];
                if (temp is ShadowNinja)
                {
                    board.CreateUnit(ShadowNinja.CopyN(t, p._X, t == Team.Player ? -1 : 125 , (ShadowNinja)UnitList[p._Y]));
                }
                else if (temp is Summoner)
                {
                    board.CreateUnit(Summoner.CopyS(t, p._X, t == Team.Player ? -1 : 125 , (Summoner)UnitList[p._Y]));
                }
                else if (temp is Unit)
                {
                    board.CreateUnit(Unit.Copy(t, p._X, t == Team.Player ? -1 : 125 , UnitList[p._Y]));
                }
                
            }
        }
        else
        {
            if (p._money >= UnitList[p._Y]._cost)
            {
                p.Spend(UnitList[p._Y]._cost);
                var temp = UnitList[p._Y];
                if (temp is ShadowNinja)
                {
                    board.CreateUnit(ShadowNinja.CopyN(t, p._X, t == Team.Player ? -1 : 125 , (ShadowNinja)UnitList[p._Y]));
                }
                else if (temp is Summoner)
                {
                    board.CreateUnit(Summoner.CopyS(t, p._X, t == Team.Player ? -1 : 125 , (Summoner)UnitList[p._Y]));
                }
                else if (temp is Unit)
                {
                    board.CreateUnit(Unit.Copy(t, p._X, t == Team.Player ? -1 : 125 , UnitList[p._Y]));
                }
                p._X = RNG.Next(0, p._selectorMaxX + 1);
                p._Y = RNG.Next(0, p._selectorMaxY + 1);
            }
        }
    }
}