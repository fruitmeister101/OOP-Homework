class Board
{
    HashSet<Unit> _activeUnits = new();
    public int _boardXSize {get;set;}
    public int _boardYSize {get;set;}
    Square[,] _boardSquares;
    List<Unit> _units;
    public Board(int x, int y)
    {
        _boardXSize = y;
        _boardYSize = x;
        InitBoard();
    }
    void InitBoard()
    {
        _boardSquares = new Square[_boardXSize, _boardYSize];
        for (int X = 0; X < _boardXSize; X++)
        {
            for (int Y = 0; Y < _boardYSize; Y++)
            {
                _boardSquares[X,Y] = new Square(X,Y);
            }
        }
        _units = new();
    }
    public string PrintBoard()
    {
        string K = "";
        for (int X = 0; X < _boardXSize; X++)
        {
            if (GM.P1._X == X){K += "->";}
            else{K += "  ";}
            K += $"|";
            for (int Y = 0; Y < _boardYSize; Y++)
            {
                K += _boardSquares[X,Y].GetSymbol();
            }
            K += "|";
            if (GM.P2._X == X){K += "<-";}
            else{K += "  ";}

            K += "\n";

        }

        return K;
    }
    public string PrintScore()
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
        var j = (GM.board._boardYSize - K.Length) / 2;
        for (int i = 0; i < j; i++)
        {
            K = " " + K;
        }
        return K;
    }
    public string PrintSelection()
    {
        string K = "\n\n";
        K += "Unit: ";
        K += $"{GM.UnitList[GM.P1._Y]._symbol} ";
        switch (GM.P1._Y)
        {
            case 0:
            K += $"Soldier - Cheap and Simple, effective in clumps. Medium speed                                                                    ";
            break;
            case 1:
            K += $"Bowman - Costly to produce, but long-ranged. Slow speed                                                                          ";
            break;
            case 2:
            K += $"Crossbowman - Very costly to produce, Ranged, and can hit nearby lanes. Slow speed, struggles in short-range                     ";
            break;
            case 3:
            K += $"Halberdier - A slower but stronger soldier, capable of hitting nearby lanes                                                      ";
            break;
            case 4:
            K += $"Scout - Cheap and Fast! Very weak                                                                                                ";
            break;
            case 5:
            K += $"Tank - Very slow, Very tough, Very expensive. Backup is recommended                                                              ";
            break;
            case 6:
            K += $"Ninja - Very Fast and Very strong with a Huge range, Teleports to attacked squares. Such great power comes with a price however  ";
            break;
            case 7:
            K += $"Summoner - Slow, but summons temporary zombies to fight for them. Zombies are mediocre, but strong in numbers                    ";
            break;
            default:
            K += "This selection does not exist yet                                                                                                 ";
            break;
        }
        K += $"\n Currency: {GM.P1._money / 10}{GM.P1._money % 10} / {GM.UnitList[GM.P1._Y]._cost}      " ;

        return K;
    }
    public string PrintKeys()
    {
        string K = "\n\nUse W and S to move the cursor up and down\nUse A and D to change selected unit";

        return K;
    }
    public Square GetSquare(int x, int y)
    {
        if (x >= _boardXSize || x < 0 || y >= _boardYSize || y < 0)
        {
            return null;
        }
        return _boardSquares[x,y];
    }
    public void ObjectMovedTo(Unit unit, int x, int y)
    {
        if (x >= _boardXSize || y >= _boardYSize || x < 0 || y < 0)
        {
            return;
        }
        _boardSquares[x,y].Occupy(unit);
    }
    public void ObjectMovedAway(Unit unit, int x, int y)
    {
        if (x >= _boardXSize || y >= _boardYSize || x < 0 || y < 0)
        {
            return;
        }
        _boardSquares[x,y].DeOccupy(unit);
    }




    public void CreateUnit(Unit unit)
    {
        _activeUnits.Add(unit);
    }
    public void DestroyUnit(Unit unit)
    {
        _activeUnits.Remove(unit);
    }
    public void UpdateAll()
    {
        for (int u = 0; u < _activeUnits.Count(); u++)
        {
            _activeUnits.ElementAt(u).Update();
        }
    }





}