class Square
{
    List<Unit> occupied;
    public delegate void Updated(Square s);
    public Updated updated;
    int x;
    int y;
    bool gotHit;

    public Square(int x, int y)
    {
        this.x = x;
        this.y = y;
        occupied = new();
        if (GM.RNG.Next(0,GM.RNG.Next(1,50)) == 0)
        /*
        if (GM.RNG.Next(0,0) == 0) // for testing purposes
        */
        {
            occupied.Add(new Unit(Team.Obstacle, x, y));
        }
    }
    public bool IsOccupied()
    {
        return occupied.Count > 0 ? true : false;
    }
    public bool IsOccupiedByEnemy(Team t)
    {
        if (occupied.Where(x => /*x.GetTeam() != Team.Obstacle &&*/ x.GetTeam() != t).Count() > 0)
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
        occupied.Add(unit);
        updated?.Invoke(this);
    }
    public void DeOccupy(Unit unit)
    {
        occupied.Remove(unit);
        updated?.Invoke(this);
    }
    public void HitThisSquare(Team team, Attack attack)
    {
        gotHit = true;
        var temp = occupied.Where(x => x.GetTeam() != team);
        if (temp.Count() > 0)
        {
            temp.ElementAt(0).TakeDmg(attack.Damage);
        }
    }
    public string GetSymbol()
    {
        if (gotHit)
        {
            gotHit = false;
            return "X";
        }
        if (occupied.Count() > 0)
        {
            return occupied[occupied.Count - 1].Symbol;
        }
        else
        {
            return "_";
        }
    }





}