class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        float time = Program.Select("\nHow long would you like to do this breathing excersize for? (In seconds)\n(Note: This excercise handles in increments on 10 seconds)");
        Initialize(time);
        Run();
    }
    protected override void Initialize(float time)
    {
        base.Initialize(time);
        _prompts.Add(new Prompt("Get Ready"));
        _prompts.Add(new Prompt("Breathe In"));
        _prompts.Add(new Prompt(" - -"));
        _prompts.Add(new Prompt("Breathe Out"));
        _prompts.Add(new Prompt("Great Job!"));
    }
    protected override void Run()
    {
        if (_time <= 0){ return; } // Basic Guard
        Program.Write("\nThis Activity is designed to help you ground your breathing and clear your mind");
        Timer(2);
        Program.Write("\n\n" + _prompts[0].GetPrompt() + "\n");
        Timer(2);
        for (int i = 0; i < Math.Clamp(_time / 10, 0, 100); i++)
        {
            BreathIn();
            BreathOut();
        }
        Program.Write("\n\n" + _prompts[4].GetPrompt() + "\n");
        Timer(3);
    }
    void BreathIn()
    {
        Program.Write("\n" + _prompts[1].GetPrompt());
        for (int i = 0; i < 5; i++)
        {
            Program.Write(_prompts[2].GetPrompt());
            Timer(1);
        }
    }
    void BreathOut()
    {
        Program.Write("\n" + _prompts[3].GetPrompt());
        for (int i = 0; i < 5; i++)
        {
            Program.Write(_prompts[2].GetPrompt());
            Timer(1);
        }
    }

}