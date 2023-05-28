using System;

class Phrase
{

    List<Word> words;

    public Phrase(){} // Here for Json Construction
    public Phrase(string phrase)
    {
        words = new List<Word>();
        var tempList = phrase.Split();
        foreach (var x in tempList)
        {
            Word temp = new Word(x);
            words.Add(temp);
        }
    }
    public bool DoneYet()
    {
        int hiddenCount = 0;
        foreach (var x in words)
        {
            if(x.IsHidden())
            {
                hiddenCount++;
            }
        }
        if (hiddenCount < words.Count()) 
        {
            return false;
        } 
        else 
        {
            return true;
        }
    }
    public void ResetHidden()
    {
        foreach (var x in words)
        {
            x.Hide(false);
        }
    }
    public void DisplayPhrase()
    {
        MS.Write("\n\n");
        foreach (var x in words)
        {
            MS.Write(x.GetWord() + " ");
        }
        MS.Write("\n\n");
    }
    public bool CheckPhraseAgainstInput(string input)
    {
        var temp = input.Split();
        if (words.Count() != temp.Count()) // if the two are not even the same length, don't even bother... Certainly no bugs here
        {
            return false;
        }
        for (int i = 0; (i < temp.Count() || i < words.Count()); i++)
        {
            if(!words[i].Compare(temp[i]))//Check our words against each
            {
                return false;
            } 
        }

        // If no errors were detected, should be a perfect match!
        return true;
    }
    public void HideRandom(int count = 0)
    {
        var rng = new Random();
        for (int i = 0; i < count; i++)
        {
            /*List<Word> unHidden = (List<Word>) // Casting stuff is fun... =D
                from word in words
                where !word.IsHidden()
                select word;*/
            List<Word> unHidden = new List<Word> ();
            foreach (var x in words)
            {
                if (!x.IsHidden())
                {
                    unHidden.Add(x);
                }
            }
            if (unHidden.Count() > 0)
            {
                int temp = rng.Next(0, (int)MathF.Max(unHidden.Count(), 0)); // The Max is there for sperious data filtering
                unHidden[temp].Hide();
            }
            

        }

    }













}