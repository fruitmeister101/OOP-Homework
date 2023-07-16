class Unit
{
    protected Team _team;
    protected int _posX;
    protected int _posY;
    protected Dictionary<Square, Square.Updated> _listeners;
    protected int _HP;
    protected int _moveSpeed;
    protected int _move = 0;
    protected Attack _attack;
    public int _lifetime;
    protected bool _scores;
    public string _symbol;
    public int _cost;
    State _state = State.Moving;
    public Unit(Team t, int x, int y, Attack a=null, int hp=1, int speed=1, int LT=-1, bool canScore=true, string symbol="☼", int cost=-1)
    {
        _listeners = new();
        _team = t;
        _posX = x;
        _posY = y;
        _symbol = symbol;
        _HP = hp;
        _moveSpeed = speed;
        _cost = cost;
        if (a == null)
        {
            _attack = new Attack(0, new int[0,0]{}, 5);
        }
        else
        {
            _attack = a;
        }
        _lifetime = LT; // For summons, mostly
        _scores = canScore; // does this unit count if it crosses the enemy line, or is it less important
        if (_team != Team.Obstacle)
        {
            GM.board.CreateUnit(this);
        }
    }
    public static Unit Copy(Team t, int x, int y, Unit u, int lt=-1)
    {
        return new Unit(t, x, y, u._attack, u._HP, u._moveSpeed, lt, lt==-1, u._symbol, u._cost);
    }
    public Team GetTeam()
    {
        return _team;
    }
    void ListenToSquare(Square square, bool x)
    {
        if (square == null){return;}
        if (x)
        {
            if (!_listeners.ContainsKey(square))
            {
                var temp = new Square.Updated(SquareInRangeUpdated);
                square._updated += temp;
                _listeners.Add(square, temp);
                SquareInRangeUpdated(square);
            }
        }
        else
        {
            square._updated -= _listeners[square];
            _listeners.Remove(square);
        }
    }
    public void SquareInRangeUpdated(Square square)
    {
        if (square.IsOccupiedByEnemy(_team))
        {
            _state = State.Attacking;
        }
        // Check how the square we are listening to has changed
    }
    void UpdateSquaresInRange()
    {
        HashSet<Square> templist = new();
        //Get all the squares in Range
        for (int X = 0; X < _attack._range.GetLength(1); X++)
        {
            for (int Y = 0; Y < _attack._range.GetLength(0); Y++)
            {
                if (_attack._range[Y,X] != 0)
                {
                    int tempX = _posX + X;
                    int tempY = _posY + + (_team == Team.Player ? Y : -Y );
                    if (tempX < GM.board._boardXSize && tempX >= 0 && tempY < GM.board._boardYSize && tempY >= 0)
                    {
                        var tempSquare = GM.board.GetSquare(_posX + X, _posY + (_team == Team.Player ? Y : -Y ));
                        if (tempSquare != null)
                        {
                            templist.Add(tempSquare);
                        }
                    }
                    if (tempX < GM.board._boardXSize && tempX >= 0 && tempY < GM.board._boardYSize && tempY >= 0)
                    {
                        var tempSquare = GM.board.GetSquare(_posX - X, _posY + (_team == Team.Player ? Y : -Y));
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
        var temp = _listeners.Where(x => !templist.Contains(x.Key));
        foreach (var s in temp)
        {
            ListenToSquare(s.Key, false);
            _listeners.Remove(s.Key);
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

    protected void Move()
    {
        if (_move !> 0)
        {
            _move--;
        }
        else
        {
            _move = _moveSpeed;
            MoveTo(GM.board.GetSquare(_posX, _posY + (_team == Team.Player ? 1 : -1)));
        }
    }
    protected void MoveTo(Square square)
    {
        GM.board.ObjectMovedAway(this, _posX, _posY);
        if (_scores)
        {
            if (_posY > GM.board._boardYSize)
            {
                GM.Tug--;
                Die();
                return;
            }
            else if (_posY < 0 && _team!=Team.Player)
            {
                GM.Tug++;
                Die();
                return;
            }
        }
        if (square == null)
        {
            _posY += (_team == Team.Player ? 1 : -1); 
            return;
        }
        var loc = square.Location();
        _posX = loc[0];
        _posY = loc[1];
        GM.board.ObjectMovedTo(this, loc[0], loc[1]);
        UpdateSquaresInRange();
    }
    protected virtual void Attack(Square square)
    {
        _attack.UseAttack(_team, square);
    }
    void UpdateLifeTime()
    {
        if (_lifetime != -1)
        {
            _lifetime--;
            if (_lifetime <= 0)
            {
                Die();
            }
        }
    }
    public void TakeDmg(int dmg)
    {
        _HP -= dmg;
        if (_HP < 1)
        {
            Die();
        }
    }
    public void Update()
    {
        if (_state == State.Attacking)
        {
            try
            {
                var s = _listeners.Where(x => x.Key.IsOccupiedByEnemy(_team));
                if (s.Count() > 0)
                {
                    Attack(s.ElementAt(0).Key);            
                }
                else
                {
                    _state= State.Moving;
                }
            }
            catch (ArgumentOutOfRangeException){_state = State.Moving;}
            catch (System.Exception){throw;}
        }
        if (_state == State.Moving)
        {
            Move();
        }
        _attack.Update();
        UpdateLifeTime();
    }
    void Die()
    {
        GM.board.ObjectMovedAway(this, _posX, _posY);
        GM.board.DestroyUnit(this);
        _posX = -1;
        _posY = -1;
    }
    // Generic Units
    static public Unit Soldier(Team team, int x, int y, int lifetime = -1)
    {
        return new Unit(team, x, y, new(3, new int[3,1]{{1},{1},{1}}, 7), 15, 2, lifetime, lifetime == -1, "§", 15);
    }
    static public Unit Bowman(Team team, int x, int y, int lifetime = -1)
    {
        return new Unit(team, x, y, new(2, new int[15,1]{{0},{0},{0},{1},{1},{1},{1},{1},{1},{1},{1},{1},{1},{1},{1}}, 10), 5, 5, lifetime, lifetime == -1, "D", 35);
    }
    static public Unit Crossbowman(Team team, int x, int y, int lifetime = -1)
    {
        return new Unit(team, x, y, new(7, new int[8,3]{{0,1,1},{0,1,1},{0,1,1},{1,1,1},{1,1,1},{1,1,1},{1,0,0},{1,0,0}}, 40), 7, 6, lifetime, lifetime == -1, "T", 55);
    }
    static public Unit Halberdier(Team team, int x, int y, int lifetime = -1)
    {
        return new Unit(team, x, y, new(4, new int[3,2]{{0,1},{1,1},{1,1}}, 5), 10, 4, lifetime, lifetime == -1, "♦", 25);
    }
    static public Unit Scout(Team team, int x, int y, int lifetime = -1)
    {
        return new Unit(team, x, y, new(1, new int[1,1]{{1}}, 3), 2, 0, lifetime, lifetime == -1, "+", 5);
    }
    static public Unit Tank(Team team, int x, int y, int lifetime = -1)
    {
        return new Unit(team, x, y, new(10, new int[1,1]{{1}}, 100), 100, 15, lifetime, lifetime == -1, "O", 55);
    }
    static public ShadowNinja Ninja(Team team, int x, int y, int lifetime = -1)
    {
        return new ShadowNinja(team, x, y, new(1000, new int[8,5]{{0,0,1,1,1},{0,0,1,1,1},{1,1,1,1,1},{1,1,1,1,1},{1,1,1,1,1},{1,1,1,1,1},{1,1,1,1,1},{1,1,1,1,1}}, 25), 1, 1, lifetime, lifetime == -1, "#", 100);
    }
    static public Summoner Summoner(Team team, int x, int y, int lifetime = -1)
    {
        return new Summoner(team, x, y, new(20, new int[8,3]{{0,0,0},{1,1,1},{1,1,1},{1,1,1},{1,1,1},{1,1,1},{1,1,1},{1,1,1}}, 35), 10, 4, lifetime, lifetime == -1, "♣", 85);
    }
    static public Unit Zombie(Team team, int x, int y, int lifetime = -1)
    {
        return new Unit(team, x, y, new(1, new int[1,1]{{1}}, 8), 10, 2, lifetime, lifetime == -1, "Z", 0);
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

