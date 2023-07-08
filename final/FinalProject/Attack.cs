class Attack
{
    DamageType Type;
    DamageTag Tag;
    int Damage;
    public int[,] Range; // Going to try something crazy here...
    // Use of the 2D Array might be complicated. To use effectively, imagine the unit's location as the 0,0'th place, then going out and down.
    // All range is Mirrored across the Unit's X Rank, so a range of 2,2 would cover 6 squares- the one he's on, then up/down and forward 1.

    public Attack(int dmg, int[,] r, DamageType type=DamageType.Physical, DamageTag tag=DamageTag.Blunt)
    {
        Damage = dmg;
        Type = type;
        Tag = tag;
        Range = r;
    }
    public  Attack BasicAttack()
    {
        return new Attack(1, new int[2,1] { {1} , {1} });
    }

}


public enum DamageType
{
    Physical,
    Magic
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