class ReflectionActivity : Activity
{
    // I can do this, I just really don't want to.
    Random rng;
    public ReflectionActivity()
    {
        
        Initialize(Program.Select("How long do you want to spend Reflecting?\n(You will not be interrupted during a question)"));
        _timeStamp = DateTime.Now;
        Run();
    }
    protected override void Initialize(float time = 0)
    {
        rng = new Random();
        base.Initialize(time);
        _prompts.Add(new Prompt("Think of a time when you stood up for someone else"));
        _prompts.Add(new Prompt("Think of a time when you did something really difficult"));
        _prompts.Add(new Prompt("Think of a time when you helped someone in need"));
        _prompts.Add(new Prompt("Think of a time when you did something truly selfless"));
        _prompts.Add(new Prompt("Why was this experience meaningful to you?"));
        _prompts.Add(new Prompt("Have you ever done anything like this before?"));
        _prompts.Add(new Prompt("How did you get started?"));
        _prompts.Add(new Prompt("How did you feel when it was complete?"));
        _prompts.Add(new Prompt("What made this time different than other times when you were not as successful?"));
        _prompts.Add(new Prompt("What is your favorite thing about this experience?"));
        _prompts.Add(new Prompt("What could you learn from this experience that applies to other situations?"));
        _prompts.Add(new Prompt("What did you learn about yourself through this experience?"));
        _prompts.Add(new Prompt("How can you keep this experience in mind in the future?"));
    }

    protected override void Run()
    {
        Program.Write("\nThis Activity is designed to help you reflect on your past and help you remember how awesome you are");

        do
        {
            DoQuestion();
        } while (GetTime() == true ? true : false);
        Timer(0.75f);
        Program.Write("\nTime's up!");
        Timer(3);
        Program.Write("\nIt has been my pleasure to help you today!");
        Timer(3);

    }
    void DoQuestion()
    {
        Timer(1f);
        Program.Write("\nQ:\n");
        Timer(1f);
        Program.Read(_prompts[rng.Next(0, 3)].GetPrompt());
        Timer(1f);
        Program.Read(_prompts[rng.Next(4, 12)].GetPrompt());
    }
    




}