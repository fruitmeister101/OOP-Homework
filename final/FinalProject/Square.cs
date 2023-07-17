class Square
{
    List<Unit> _occupied;
    public delegate void Updated(Square s);
    public Updated _updated;
    int _x;
    int _y;
    bool _gotHit;

    public int[] Location()
    {
        return new int[2] {_x, _y};
    }
    public Square(int x, int y)
    {
        _x = x;
        _y = y;
        _occupied = new();
        if (GM.RNG.Next(0,GM.RNG.Next(1,50)) == 0)
        /*
        if (GM.RNG.Next(0,0) == 0) // for testing purposes
        */
        {
            _occupied.Add(new Unit(Team.Obstacle, x, y));
        }
    }
    public bool IsOccupied()
    {
        return _occupied.Count > 0 ? true : false;
    }
    public bool IsOccupiedByEnemy(Team t)
    {
        if (_occupied.Where(x => x.GetTeam() != /*Team.Obstacle && x.GetTeam() !=*/ t).Count() > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Occupy(Unit unit)
    {
        _occupied.Add(unit);
        _updated?.Invoke(this);
    }
    public void DeOccupy(Unit unit)
    {
        _occupied.Remove(unit);
       // _updated?.Invoke(this);
    }
    public virtual void HitThisSquare(Team team, Attack attack)
    {
        _gotHit = true;
        var temp = _occupied.Where(x => x.GetTeam() != team);
        if (temp.Count() > 0)
        {
            temp.ElementAt(0).TakeDmg(attack._damage);
        }
    }
    public string GetSymbol()
    {
        if (_gotHit)
        {
            _gotHit = false;
            return "X";
        }
        if (_occupied.Count() > 0)
        {
            return _occupied[_occupied.Count - 1]._symbol;
        }
        else
        {
            return "_";
        }
    }





}