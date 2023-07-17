class Interface
{
    public bool _auto;
    public int _X;
    public int _Y;
    public int _money=0;
    (char,char,char,char) _chars; // Follow the convention: UP DOWN LEFT RIGHT
    public int _selectorMaxX;
    public int _selectorMaxY;
    public List<Unit> _units;
    public Interface(int maxX, int maxY, (char,char,char,char) keys, bool auto=false)
    {
        _chars = keys;
        _selectorMaxX = Math.Max(0, maxX);
        _X = _selectorMaxX/2;
        _selectorMaxY = Math.Max(0, maxY);
        _units = new();
        _auto = auto;
    }
    public void DoStuff(char? k = null)
    {
        _money++;
        if (k == _chars.Item1)
        {
            _X--;
        }
        else if (k == _chars.Item2)
        {
            _X++;
        }
        else if (k == _chars.Item3)
        {
            _Y--;
        }
        else if (k == _chars.Item4)
        {
            _Y++;
        }
        if (_auto && k != null && k != ' ' && k != ',' && _chars.ToString().Contains((char)k))
        {
            var temp = _chars.ToString();
            _auto = false;
        }
        WrapSelectors();
    }
    void WrapSelectors()
    {
        if (_Y > _selectorMaxY)
        {
            _Y = 0;
        }
        if (_Y < 0)
        {
            _Y = _selectorMaxY;
        }
        if (_X > _selectorMaxX)
        {
            _X = 0;
        }
        if (_X < 0)
        {
            _X = _selectorMaxX;
        }
    }
    public void Spend(int amount)
    {
        _money -= amount;
    }
}