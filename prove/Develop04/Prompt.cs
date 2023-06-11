class Prompt
{
    string _prompt {get; set;}
    bool _useInRandom {get; set;} // I was hoping this would be more useful than it ended up being...
    public Prompt(){} // For Json-ing
    public Prompt(string prompt, bool useInRandom=true)
    {
        _prompt = prompt;
        _useInRandom =useInRandom;
    }
    public string GetPrompt()
    {
        return _prompt;
    }
    public bool GetRand()
    {
        return _useInRandom;
    }



}