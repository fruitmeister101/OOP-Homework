class ListingActivity : Activity
{
    // I WANT THIS TO BE OVERRRRRR
    Random rng;
    public ListingActivity()
    {
        Initialize(Program.Select("How long do you want to spend Listing?\n(You will not be interrupted during a prompt)"));
        _timeStamp = DateTime.Now;
        Run();
    }


    protected override void Initialize(float time)
    {
        rng = new Random();
        base.Initialize(time);
        _prompts.Add(new Prompt("Who are people that you appreciate?"));
        _prompts.Add(new Prompt("What are personal strengths of yours?"));
        _prompts.Add(new Prompt("Who are people that you have helped this week?"));
        _prompts.Add(new Prompt("When have you felt the Holy Ghost this month?"));
        _prompts.Add(new Prompt("Who are some of your personal heroes?"));
    }


    protected override void Run()
    {
        Program.Write("\n\nThis Activity is designed to help you introspect.\nThat is to say, to know yourself better\n");
        Timer(3f);
        Program.Write(_prompts[rng.Next(0, _prompts.Count())].GetPrompt());
        do
        {
            Timer(1);
            Program.Read("");
        } while (GetTime() == true ? true : false);

        Timer(2);
        Program.Write("\n\nI hope this has been enlightening");
        Timer(3f);
        Program.Write("\n=D");
        Timer(2);

    }


}