class Interface
{
    public int X;
    public int Y;
    public int money=0;
    (char,char,char,char) chars; // Follow the convention: UP DOWN LEFT RIGHT
    int selectorMaxX;
    int selectorMaxY;
    public List<Unit> units;
    public Interface(int maxX, int maxY, (char,char,char,char) keys)
    {
        chars = keys;
        selectorMaxX = Math.Max(0, maxX);
        selectorMaxY = Math.Max(0, maxY);
        units = new();
    }


    public void DoStuff()
    {
        money++;
        if (!Console.KeyAvailable){}
        else
        {
            char k = Program.Read();
            if (k == chars.Item1)
            {
                X--;
            }
            if (k == chars.Item2)
            {
                X++;
            }
            if (k == chars.Item3)
            {
                Y++;
            }
            if (k == chars.Item4)
            {
                Y--;
            }
            WrapSelectors();
        }
    }

    void WrapSelectors()
    {
        if (Y > selectorMaxY)
        {
            Y = 0;
        }
        if (Y < 0)
        {
            Y = selectorMaxY;
        }
        if (X > selectorMaxX)
        {
            X = 0;
        }
        if (X < 0)
        {
            X = selectorMaxX;
        }
    }

    public void Spend(int amount)
    {
        money -= amount;
    }


}