class ShadowNinja : Unit
{
    public ShadowNinja(Team t, int x, int y, Attack a=null, int hp=1, int speed=1, int LT=-1, bool canScore=true, string symbol="☼", int cost=-1) : base (t, x, y, a, hp, speed, LT, canScore, symbol, cost)
    {}
    public static ShadowNinja CopyN(Team t, int x, int y, ShadowNinja u, int lt=-1)
    {
        return new ShadowNinja(t, x, y, u._attack, u._HP, u._moveSpeed, lt, lt==-1, u._symbol, u._cost);
    }


    protected override void Attack(Square square)
    {
        if (_attack.UseAttack(_team, square))
        {
            MoveTo(square);
        }
    }  
}