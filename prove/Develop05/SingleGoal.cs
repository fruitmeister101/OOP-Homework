class SingleGoal : Goal
{
    public SingleGoal(){}
    public SingleGoal(string goal, int p)
    {
        theGoal = goal;
        points = (p <= 0 ? 100 : p);
    }
    public override int Accomplished()
    {

        Program.DeleteGoal(this);
        return base.Accomplished();
    }
}