using System;

class Program
{
    static int score = 0;
    static string selections = "\n0: Quit\n1: Create Single-time Goal\n2: Create Short Goal (repeated x times)\n3: Create Long Goal (foreseeable future)\n4: List Goals\n5: Remove Goal\n6: Accomplish Goal\n7: Reset All\n";
    static List<Goal> goals = new List<Goal>();
    static void Main(string[] args)
    {
        // And SO it begins...
        Console.Clear();
        int sel = -1;

        int failsafe = 0;

        do
        {
            
            sel = Menu();
            string text;
            int p = 0;
            int x = 0;
            Stuff.Write(); // for an extra line
            switch (sel)
            {
                case 0: // Quit
                break;
                case 1: // Create Single-time Goal
                    text = Stuff.Read("Insert Goal Name");
                    p = Stuff.Select("How many Points for accomplishing?", false, "Unknown Input - AutoScore 100");
                    goals.Add(new SingleGoal(text, p));
                    Stuff.Write("Success!\n\n");
                break;
                case 2: // Create Short Goal
                    text = Stuff.Read("Goal Text");
                    p = Stuff.Select("How many Points for accomplishing?", false, "Unknown Input - AutoScore 100");
                    x = Stuff.Select("How many Times do you want to see this Goal?", false, "Unknown Input - AutoMulti 1");
                    goals.Add(new ShortGoal(text, p, x));
                    Stuff.Write("Success!\n\n");
                break;
                case 3: // Create Long Goal
                    text = Stuff.Read("Goal Text");
                    p = Stuff.Select("How many Points for accomplishing?", false, "Unknown Input - AutoScore 100");
                    goals.Add(new SingleGoal(text, p));
                    Stuff.Write("Success!\n\n");
                break;
                case 4: // List Goals
                    ListGoals();
                break;
                case 5: // Remove Goal
                    DeleteGoal( Stuff.Select("Goal Number to be removed", false, "Unknown Input\n"));
                break;
                case 6: // Accomplish Goal
                    int i = Stuff.Select("Which goal did you accomplish?");
                    i -= 1;
                    if (i >= 0 && i < goals.Count())
                    {
                        AddPoints(goals[i].Accomplished());
                        break;
                    }
                    Stuff.Write("\n\nGoal did not appear in the list\n");
                break;
                case 7: // Reset All
                    if (Stuff.Select("Are you Sure you want to reset?\nType '321' to be sure") == 321)
                    {
                        Stuff.Write("Poof! It's all gone");
                        goals = new List<Goal>();
                        score = 0;
                    }
                break;
                default:
                    failsafe++;
                break;
            }
            
        } while (sel != 0 && failsafe < 10);


        Console.Clear();
        Stuff.Write($"You Finished with a score of {score}!");





        
    }

    public static void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }
    public static void DeleteGoal(Goal goal)
    {
        if (goals.Remove(goal))
        {
            Stuff.Write("\n--------------------\nSuccessfully Removed\n--------------------\n");
        }
        else
        {
            Stuff.Write("\n\nGoal did not appear in the list");
        }
    }
    public static void DeleteGoal(int i)
    {
        i -= 1; // For Users
        if (i >= 0 && i < goals.Count())
        {
            DeleteGoal(goals[i]);
        }
        Stuff.Write("\n\nGoal did not appear in the list\n");
    }
    public static void ListGoals()
    {
        int n = 1;
        Stuff.Write("\n\nCurrent Goals:\n");
        foreach (Goal g in goals)
        {
            Stuff.Write($"{n}: " + g.GetGoalText() + " (" + g.GetScore() + "P)" + "\n");
            n++;
        }
    }
    public static void AddPoints(int p)
    {
        score += p;
    }
    static int Menu()
    {
        Stuff.Write(selections);
        Stuff.Write($"Current Score: {score}");
        return Stuff.Select("\nMake Your Selection from the numbers listed Above");
    }
}