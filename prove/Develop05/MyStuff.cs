static class Stuff
{
    // Rip all the things from this file you find necessary. These are just convenient to have around.
    public static void Write(string text="")
    {
        Console.Write(text);
    }
    public static string Read(string text="")
    {
        Console.Write(text + "\n: ");
        var x = Console.ReadLine();
        return x == null ? "" : x ; // Just in case it does happen somehow, I'd rather have it sanitized
    }
    public static int Select(string text="", bool supressError=false, string manualError="") // String to Int parser, Very handy
    {
        int x;
        var temp = Read(text);
        try
        {
            x = Int32.Parse(temp);
            return x;
        }
        catch (FormatException)
        {
            if (!supressError)
            {
                if (manualError == "")
                {
                    Write("\nI'm sorry, what you input is invalid, please try again.\n");
                }
                else
                {
                    Write(manualError + "\n");
                }
            }
        }
        catch
        {
            throw;
        }

        return -1;
    }
}