class Summoner : Unit
{
    Unit _unit = Unit.Zombie(Team.Obstacle,-1,-1,45);
    public Summoner(Team t, int x, int y, Attack a=null, int hp=1, int speed=1, int LT=-1, bool canScore=true, string symbol="☼", int cost=-1) : base (t, x, y, a, hp, speed, LT, canScore, symbol, cost)
    {}
    public static Summoner CopyS(Team t, int x, int y, Summoner u, int lt=-1)
    {
        return new Summoner(t, x, y, u._attack, u._HP, u._moveSpeed, lt, lt==-1, u._symbol, u._cost);
    }

    protected override void Attack(Square square)
    {
        if (_attack.UseAttack(_team, GM.board.GetSquare(square.Location()[0], _posY), false))
        {
            GM.board.CreateUnit(Unit.Copy(_team, square.Location()[0], _posY, _unit, 55));
        }
        


    }

}