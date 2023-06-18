class ShortGoal : Goal
{
    int howManyTimes = 1;
    public ShortGoal(){}
    public ShortGoal(string goal, int p, int x)
    {
        theGoal = goal;
        points = (p <= 0 ? 100 : p);
        howManyTimes = (x <= 0 ? 1 : x);
    }
    public override int Accomplished()
    {
        howManyTimes--;
        if (howManyTimes <= 0)
        {
            Program.DeleteGoal(this);
        }
        return base.Accomplished();
    }
    public override string GetGoalText()
    {
        return base.GetGoalText() + $" x{howManyTimes}";
    }
}