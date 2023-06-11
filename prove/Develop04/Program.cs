class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Run();
    }
    static void Run()
    {
        int count = 0;
        do
        {  
            Thread.Sleep(3000);
            Console.Clear();
            if (count > 0){ count--; } // Decrement the counter each time
            switch (Select("What would you like to do today? (Select via Number)\n0: Quit\n1: Breathing Excercise\n2: Listing Activity\n3: Reflection Activity"))
            {
                case 1: 
                    
                    new BreathingActivity();
                break;
                case 2: 
                    new ListingActivity();
                break;
                case 3: 
                    new ReflectionActivity();
                break;
                case 0: 
                    count = 20;
                break;            

                default:
                    count++; // Double increment on Garbage-in
                    count++;
                break;

            }
            if (count > 10)
            {
                Write("\n\nThank you for your visit today!");
                Thread.Sleep(3000);
                Write("\n\n");
                return;
            }
            
        } while (count < 15); // Secondary Failsafe


    }

    public static void Write(string text="")
    {
        Console.Write(text);
    }
    public static string Read(string text="")
    {
        Console.Write(text + "\n: ");
        var x =Console.ReadLine();
        return x == null ? "" : x ; // Just in case it does happen somehow, I'd rather have it sanitized
    }
    public static int Select(string text="") // String to Int parser, Very handy
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
            Write("\nI'm sorry, what you input is invalid, please try again.\n");
        }
        catch
        {
            throw;
        }

        return -1;
    }
}