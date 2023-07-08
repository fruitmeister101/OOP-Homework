class Square
{
    List<Unit> occupied;
    public delegate void Updated(Square s);
    public Updated updated;

    public Square()
    {
        occupied = new();

        if (GM.RNG.Next(0, 50) == 0)
        {
            occupied.Add(new Unit(Team.Obstacle, -1, -1));
        }
    }
    public bool IsOccupied()
    {
        return occupied.Count > 0 ? true : false;
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
    public void HitThisSquare(int dmg, bool areaDmg=false)
    {

    }
    public string GetSymbol()
    {
        return occupied[occupied.Count - 1].Symbol;
    }





}