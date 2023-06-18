class Goal
{

    protected string theGoal {get; set;}
    protected int points {get; set;}

    public Goal(){}
    public Goal(string goal, int p)
    {
        theGoal = goal;
        points = (p == 0 ? 100 : p);
    }
    public int GetScore()
    {
        return points;
    }
    public virtual string GetGoalText()
    {
        return theGoal;
    }
    public virtual int Accomplished()
    {
        Stuff.Write($"\n\nHurray!!!\nYou get {points} points!\n\n");
        return points;
    }




}