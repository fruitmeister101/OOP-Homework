

class Word
{
    string word {get; set;} // What is my word?
    bool hide {get; set;} // Am I hidden?
    public Word(){} // Here for Json Construction
    public Word(string word)
    {
        this.word = word;
    }
    public string GetWord()
    {
        if (hide) // If we are hidden, return invisible
        {
            return "__?__";
        }
        else
        {
            return word != null ? word : "!!Empty!!"; // null detector for bad construction.
        }
    }
    public bool Compare(string word)
    {
        if (word.ToLower() == this.word.ToLower()) // Not case sensitive
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsHidden(){ return hide; }
    public void Hide(bool hide = true) { this.hide = hide; }
}