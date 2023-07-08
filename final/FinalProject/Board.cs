class Board
{
    List<Unit> ActiveUnits = new();
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
                BoardSquares[X,Y] = new Square();
            }
        }
        Units = new();
    }
    public void PrintBoard()
    {
        string K = "";
        for (int X = 0; X < boardXSize; X++)
        {
            for (int Y = 0; Y < boardYSize; Y++)
            {
                if (BoardSquares[X,Y].IsOccupied())
                {
                    // Do some logic to determine what needs to be displayed
                    K += "☼";
                }
                else
                {
                    K += " ";
                }
            }
            K += "\n";
            
        }

        Program.Print(K);
    }
    public Square GetSquare(int x, int y)
    {
        return BoardSquares[x,y];
    }
    public void ObjectMovedTo(Unit unit, int x, int y)
    {
        BoardSquares[x,y].Occupy(unit);
    }
    public void ObjectMovedAway(Unit unit, int x, int y)
    {
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
            ActiveUnits[u].Update();
        }
    }





}