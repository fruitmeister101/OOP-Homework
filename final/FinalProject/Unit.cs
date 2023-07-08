class Unit
{
    Team team;
    int posX;
    int posY;
    List<SquareListener> squareListeners;
    HashSet<Square> squaresInRange;
    int HP;
    int MoveSpeed;
    Attack[] attacks;
    int Lifetime;
    bool Scores;
    public string Symbol;
    public Unit(Team t, int x, int y, Attack[] a=null, int hp=1, int s=1, int LT=-1, bool c=true, string symbol="☼")
    {
        squareListeners = new();
        squaresInRange = new();

        team = t;
        posX = x;
        posY = y;
        Symbol = symbol;
        HP = hp;
        MoveSpeed = s;
        if (a == null)
        {
            attacks = new Attack[0] { };
        }
        else
        {
            attacks = a;
        }
        Lifetime = LT; // For summons, mostly
        Scores = c; // does this unit count if it crosses the enemy line, or is it less important

        if (team != Team.Obstacle)
        {
            GM.board.CreateUnit(this);
        }
        
    }



    public void SquareInRangeUpdated(Square square)
    {
        // Check how the square we are listening to has changed
    }
    void UpdateSquaresInRange()
    {
        HashSet<Square> templist = new();
        foreach (var attack in attacks)
        {
            for (int X = 0; X < attack.Range.GetLength(1); X++)
            {
                for (int Y = 0; Y < attack.Range.GetLength(0); Y++)
                {
                    if (attack.Range[X,Y] != 0)
                    {
                        
                    }
                }
            }
            
        }
    }

    void Move()
    {
        GM.board.ObjectMovedAway(this, posX, posY);
        posX += team == Team.Player ? 1 : -1;
        if (Scores)
        {
            if (posX > GM.board.boardXSize)
            {
                GM.Tug++;
            }
            else if (posX < 0)
            {
                GM.Tug--;
            }
        }
        GM.board.ObjectMovedTo(this, posX, posY);
    }
    void Attack(Square s)
    {

    }
    void UpdateLifeTime()
    {
        if (Lifetime != -1)
        {
            Lifetime--;
        }
    }
    public void TakeDmg(int dmg)
    {
        HP -= dmg;
        if (HP < 1)
        {
            Die();
        }
    }
    public virtual void Update()
    {

    }
    void Die()
    {
        GM.board.ObjectMovedAway(this, posX, posY);
        GM.board.DestroyUnit(this);
        posX = -1;
        posY = -1;
    }

}

enum Team
{
    Player,
    Enemy,
    Obstacle
}

