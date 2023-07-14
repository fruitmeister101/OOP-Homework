class Attack
{
    public DamageType Type;
    public DamageTag Tag;
    public int Damage;
    int cooldown;
    int maxCooldown;
    public int[,] Range; // Going to try something crazy here...
    // Use of the 2D Array might be complicated. To use effectively, imagine the unit's location as the 0,0'th place, then going out and down.
    // All range is Mirrored across the Unit's X Rank, so a range of 2,2 would cover 6 squares- the one he's on, then up/down and forward 1.

    public Attack(int dmg, int[,] r, int cd, DamageType type=DamageType.Physical, DamageTag tag=DamageTag.Blunt)
    {
        Damage = dmg;
        Type = type;
        Tag = tag;
        Range = r;
        maxCooldown = cd;
    }
    public Attack BasicAttack()
    {
        return new Attack(1, new int[2,1] { {1} , {1} }, 3);
    }

    public void UseAttack(Team t, Square s)
    {
        if (cooldown <= 0)
        {
            s.HitThisSquare(t, this);
            cooldown = maxCooldown;
        }
        else
        {
            Update();
        }
    }
    public void Update()
    {
        cooldown--;
    }
}


public enum DamageType
{
    Physical,
    Magic,
    Summon
}
public enum DamageTag
{
    Blade,
    Spear,
    Blunt,
    Explosion,
    Mystery,
    True
}