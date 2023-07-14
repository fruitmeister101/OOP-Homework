class Unit
{
    Team team;
    int posX;
    int posY;
    Dictionary<Square, Square.Updated> Listeners;
    int HP;
    int MoveSpeed;
    int move;
    Attack attack;
    int Lifetime;
    bool Scores;
    public string Symbol;
    public int Cost;
    State state = State.Moving;
    public Unit(Team t, int x, int y, Attack a=null, int hp=1, int speed=1, int LT=-1, bool canScore=true, string symbol="☼", int cost=-1)
    {
        Listeners = new();

        team = t;
        posX = x;
        posY = y;
        Symbol = symbol;
        HP = hp;
        MoveSpeed = speed;
        Cost = cost;
        if (a == null)
        {
            attack = new Attack(0, new int[0,0]{}, 5);
        }
        else
        {
            attack = a;
        }
        Lifetime = LT; // For summons, mostly
        Scores = canScore; // does this unit count if it crosses the enemy line, or is it less important

        if (team != Team.Obstacle)
        {
            GM.board.CreateUnit(this);
        }
        //GM.board.GetSquare(x,y).Occupy(this);
        
    }
    public static Unit Copy(Team t, int x, int y, Unit u, int lt=-1)
    {
        return new Unit(t, x, y, u.attack, u.HP, u.MoveSpeed, lt, lt==-1, u.Symbol, u.Cost);
    }
    public Team GetTeam()
    {
        return team;
    }

    void ListenToSquare(Square square, bool x)
    {
        if (square == null){return;}
        if (x)
        {
            if (!Listeners.ContainsKey(square))
            {
                var temp = new Square.Updated(SquareInRangeUpdated);
                square.updated += temp;
                Listeners.Add(square, temp);
                SquareInRangeUpdated(square);
            }
        }
        else
        {
            square.updated -= Listeners[square];
            Listeners.Remove(square);
        }
    }
    public void SquareInRangeUpdated(Square square)
    {
        if (square.IsOccupiedByEnemy(team))
        {
            state = State.Attacking;
        }
        // Check how the square we are listening to has changed
    }
    void UpdateSquaresInRange()
    {
        HashSet<Square> templist = new();
        //Get all the squares in Range
        for (int X = 0; X < attack.Range.GetLength(1); X++)
        {
            for (int Y = 0; Y < attack.Range.GetLength(0); Y++)
            {
                if (attack.Range[Y,X] != 0)
                {
                    int tempX = posX + X;
                    int tempY = posY + + (team == Team.Player ? Y : -Y );
                    if (tempX < GM.board.boardXSize && tempX >= 0 && tempY < GM.board.boardYSize && tempY >= 0)
                    {
                        var tempSquare = GM.board.GetSquare(posX + X, posY + (team == Team.Player ? Y : -Y ));
                        if (tempSquare != null)
                        {
                            templist.Add(tempSquare);
                        }
                    }
                    if (tempX < GM.board.boardXSize && tempX >= 0 && tempY < GM.board.boardYSize && tempY >= 0)
                    {
                        var tempSquare = GM.board.GetSquare(posX - X, posY + (team == Team.Player ? Y : -Y));
                        if (tempSquare != null)
                        {
                            templist.Add(tempSquare);
                        }
                    }
                }
            }
        }

        // Begin Listeners on New Squares
        foreach (var s in templist)
        {
            ListenToSquare(s, true);
        }

        // Scrub the list of outdated squares
        /*for (int i = 0; i < temp.Count(); i++)
        {
            ListenToSquare(temp.ElementAt(i).Key, false);
            Listeners.Remove(temp.ElementAt(i).Key);
        }*/
        var temp = Listeners.Where(x => !templist.Contains(x.Key));
        foreach (var s in temp)
        {
            ListenToSquare(s.Key, false);
            Listeners.Remove(s.Key);
        }

        /*
        for (int i = 0; i < squaresInRange.Count(); i++)
        {
            if (templist.Contains(squaresInRange.ElementAt(i))!)
            {
                squaresInRange.Remove(squaresInRange.ElementAt(i));
                i--;
            }
        }*/
    }

    void Move()
    {
        if (move !> 0)
        {
            move--;
        }
        else
        {
            move = MoveSpeed;       
            GM.board.ObjectMovedAway(this, posX, posY);
            posY += (team == Team.Player ? 1 : -1);
            if (Scores)
            {
                if (posY > GM.board.boardYSize)
                {
                    GM.Tug--;
                    Die();
                }
                else if (posY < 0)
                {
                    GM.Tug++;
                    Die();
                }
            }
            GM.board.ObjectMovedTo(this, posX, posY);
            UpdateSquaresInRange();
        }
    }
    void Attack(Square square)
    {
        attack.UseAttack(team, square);
    }
    void UpdateLifeTime()
    {
        if (Lifetime != -1)
        {
            Lifetime--;
            if (Lifetime <= 0)
            {
                Die();
            }
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
        if (state == State.Attacking)
        {
            try
            {
                var s = Listeners.Where(x => x.Key.IsOccupiedByEnemy(team));
                if (s.Count() > 0)
                {
                    Attack(s.ElementAt(0).Key);            
                }
                else
                {
                    state= State.Moving;
                }
            }
            catch (ArgumentOutOfRangeException){state = State.Moving;}
            catch (System.Exception){throw;}
        }
        if (state == State.Moving)
        {
            Move();
            attack.Update();
        }
        UpdateLifeTime();
    }
    void Die()
    {
        GM.board.ObjectMovedAway(this, posX, posY);
        GM.board.DestroyUnit(this);
        posX = -1;
        posY = -1;
    }





    // Generic Units

    static public Unit Soldier(Team team, int x, int y, int lifetime = -1)
    {
        return new Unit(team, x, y, new(3, new int[2,1]{{1},{1}}, 4), 15, 2, lifetime, lifetime == -1, "§", 7);
    }
    static public Unit Bowman(Team team, int x, int y, int lifetime = -1)
    {
        return new Unit(team, x, y, new(2, new int[15,1]{{0},{1},{1},{1},{1},{1},{1},{1},{1},{1},{1},{1},{1},{1},{1}}, 5), 5, 3, lifetime, lifetime == -1, "D", 12);
    }
    static public Unit Crossbowman(Team team, int x, int y, int lifetime = -1)
    {
        return new Unit(team, x, y, new(2, new int[8,3]{{0,1,1},{0,1,1},{0,1,1},{1,1,1},{1,1,1},{1,1,1},{1,0,0},{1,0,0},}, 5), 7, 4, lifetime, lifetime == -1, "T", 12);
    }
    static public Unit Halberdier(Team team, int x, int y, int lifetime = -1)
    {
        return new Unit(team, x, y, new(4, new int[3,2]{{0,1},{0,1},{1,1}}, 5), 10, 5, lifetime, lifetime == -1, "♦", 15);
    }
    static public Unit Scout(Team team, int x, int y, int lifetime = -1)
    {
        return new Unit(team, x, y, new(1, new int[1,1]{{1}}, 1), 2, 0, lifetime, lifetime == -1, "+");
    }
}

enum Team
{
    Player,
    Enemy,
    Obstacle
}
enum State
{
    Moving,
    Attacking
}
enum Resistance
{
    Physical,
    Magic,
    Blade,
    Spear,
    Blunt,
    Explosion,
    Mystery,
}

