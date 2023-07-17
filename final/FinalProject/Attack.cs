class Attack
{
    public DamageType _type;
    public DamageTag _tag;
    public int _damage;
    int _cooldown = 0;
    int _maxCooldown;
    public int[,] _range; // Going to try something crazy here...
    // Use of the 2D Array might be complicated. To use effectively, imagine the unit's location as the 0,0'th place, then going out and down.
    // All range is Mirrored across the Unit's X Rank, so a range of 2,2 would cover 6 squares- the one he's on, then up/down and forward 1.

    public Attack(int dmg, int[,] r, int cd, DamageType type=DamageType.Physical, DamageTag tag=DamageTag.Blunt)
    {
        _damage = dmg;
        _type = type;
        _tag = tag;
        _range = r;
        _maxCooldown = cd;
    }
    public Attack(Attack a)
    {
        _damage = a._damage;
        _type = a._type;
        _tag = a._tag;
        _range = a._range;
        _maxCooldown = a._maxCooldown;
    }
    public Attack BasicAttack()
    {
        return new Attack(1, new int[2,1] { {1} , {1} }, 3);
    }

    public bool UseAttack(Team t, Square s, bool actuallyHit=true)
    {
        if (_cooldown <= 0)
        {
            
            
            
            if (actuallyHit)
            {
                s.HitThisSquare(t, this);
            }

            _cooldown = _maxCooldown;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Update()
    {
        if (_cooldown > 0)
        {
            _cooldown--;
        }
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