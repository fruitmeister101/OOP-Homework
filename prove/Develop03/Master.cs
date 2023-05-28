

static class MS
{

    public static void Write(string x)
    {
        Console.Write(x);
    }
    public static string Read(string x = "")
    {
        Console.Write(x + "\n: ");
        var temp = Console.ReadLine();
        temp = temp == null ? "" : temp ; // null detection
        return temp;
    }
    public static void Test(Phrase phrase)
    {
        string input = "";
        int onARollCounter = 0; // Doing better will hurry up the process
        bool done; 
        do
        {
            done = phrase.DoneYet();
            Console.Clear();
            phrase.DisplayPhrase();

            input = Read();
            if (input.ToLower() == "q")
            {
                break;
            }
            if (phrase.CheckPhraseAgainstInput(input))
            {
                onARollCounter++;
                phrase.HideRandom(onARollCounter);
            }
            else
            {
                onARollCounter--;
            }



            onARollCounter = Math.Clamp(onARollCounter, 0, 4);
        } while (!done); // While you have not gotten it
    }







}