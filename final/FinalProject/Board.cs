class Board
{
    HashSet<Unit> ActiveUnits = new();
    public int boardXSize {get;set;}
    public int boardYSize {get;set;}
    Square[,] BoardSquares;
    List<Unit> Units;
    public Board(int x, int y)
    {
        boardXSize = y;
        boardYSize = x;
        InitBoard();
    }
    void InitBoard()
    {
        BoardSquares = new Square[boardXSize, boardYSize];
        for (int X = 0; X < boardXSize; X++)
        {
            for (int Y = 0; Y < boardYSize; Y++)
            {
                BoardSquares[X,Y] = new Square(X,Y);
            }
        }
        Units = new();
    }
    public void PrintBoard()
    {
        string K = "";
        for (int X = 0; X < boardXSize; X++)
        {
            if (GM.P1.X == X){K += "->";}
            else{K += "  ";}
            K += $"|";
            for (int Y = 0; Y < boardYSize; Y++)
            {
                K += BoardSquares[X,Y].GetSymbol();
            }
            K += "|";
            if (GM.P2.X == X){K += "<-";}
            else{K += "  ";}

            K += "\n";

        }

        Program.Print(K);
    }
    public void PrintScore()
    {
        string K = "";
        for (int i = 0; i < GM.TugMax * 2; i++)
        {
            if (GM.Tug + i < GM.TugMax)
            {
                K += " ♦";
            }
            else
            {
                K += " -";
            }
        }
        var j = (GM.board.boardYSize - K.Length) / 2;
        for (int i = 0; i < j; i++)
        {
            K = " " + K;
        }
        Program.Print("\n" + K);
    }
    public Square GetSquare(int x, int y)
    {
        if (x > boardXSize || x < 0 || y > boardYSize || y < 0)
        {
            return null;
        }
        return BoardSquares[x,y];
    }
    public void ObjectMovedTo(Unit unit, int x, int y)
    {
        if (x >= boardXSize || y >= boardYSize || x < 0 || y < 0)
        {
            return;
        }
        BoardSquares[x,y].Occupy(unit);
    }
    public void ObjectMovedAway(Unit unit, int x, int y)
    {
        if (x >= boardXSize || y >= boardYSize || x < 0 || y < 0)
        {
            return;
        }
        BoardSquares[x,y].DeOccupy(unit);
    }




    public void CreateUnit(Unit unit)
    {
        ActiveUnits.Add(unit);
    }
    public void DestroyUnit(Unit unit)
    {
        ActiveUnits.Remove(unit);
    }
    public void UpdateAll()
    {
        for (int u = 0; u < ActiveUnits.Count(); u++)
        {
            ActiveUnits.ElementAt(u).Update();
        }
    }





}