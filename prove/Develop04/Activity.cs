class Activity
{
    protected DateTime _timeStamp {get; set;}
    protected float _time {get; set;}
    protected List<Prompt> _prompts {get; set;}
    protected void Timer(float time=0, string startMessage="", string endMessage="") // Set a timer for [time] seconds
    {
        Thread.Sleep((int)(time * 1000));
    }
    public Activity() {}
    protected virtual void Initialize(float time=0)
    {
        _prompts = new List<Prompt>();
        _time = time;
    }
    protected void AddPrompt(Prompt prompt)
    {
        _prompts.Add(prompt);
    }
    protected virtual void Run() {}
    protected bool? GetTime()
    {
        if (_timeStamp == null){ return null; } // Basic guard
        float s = DateTime.Now.Second - _timeStamp.Second;
        float m = DateTime.Now.Minute - _timeStamp.Minute;
        float h = DateTime.Now.Hour - _timeStamp.Hour;

        float temp = s + (60 * m) + (3600 * h);

        return temp < _time;
    }

}