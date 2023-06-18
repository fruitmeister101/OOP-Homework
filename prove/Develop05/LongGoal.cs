class LongGoal : Goal
{
    // I think the Base Class handles everything here...?
    public LongGoal(){}
    public LongGoal(string goal, int p)
    {
        theGoal = goal;
        points = (p <= 0 ? 100 : p);
    }
}