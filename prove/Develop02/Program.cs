using System.Text.Json;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Develop02 World!");

        RunJournal(); // main loop
    }

    static void RunJournal() // Main Loop
    {
        Journal journal = new Journal();
        bool exit = false; // while true do
        while (!exit)
        {
            int i = -1; // basic selector;
            if // I know this is hideous...
            (!Int32.TryParse(Read("Please Select:\n" +
                            "1: Load journal from file\n" +
                            "2: Create a new entry\n" +
                            "3: Edit an existing entry\n" +
                            "4: Delete an existing entry\n" +
                            "5: Display single entry\n" +
                            "6: Display all entries\n" +
                            "7: Save journal to file\n"), out i))
            {/*Parse Failed*/ i = -1; }
            switch (i)
            {
                case 1:
                    var temp =Journal.LoadFromFile(Read("Name of File: "));
                    journal = temp == null ? journal : temp ;
                break;
                case 2:
                    journal.AddNewEntry(false);
                break;
                case 3:
                    i = -1;
                    Int32.TryParse(Read("Entry Number: "), out i);
                    journal.entries[i - 1].Edit();
                break;
                case 4:
                    i = -1;
                    Int32.TryParse(Read("Entry Number: "), out i);
                    journal.DeleteEntry(i - 1);
                break;
                case 5:
                    i = -1;
                    Int32.TryParse(Read("Entry Number: "), out i);
                    journal.entries[i - 1].Display();
                break;
                case 6:
                    for (int x = 0; x < journal.entries.Count(); x++)
                    {
                        Entry entry = journal.entries[x];
                        Write($"\n{x + 1}: ");
                        entry.Display();
                    }
                break;
                case 7:
                    journal.SaveToFile(Read("Name of File: "));
                break;
                default:
                    exit = true;
                    if (Read("\nUnrecognized input: Going to exit\nAre you sure you want to exit? (type anything to cancel) :") != "")
                    {
                        exit = false;
                    }
                break;
                
            }
        }
    }

    public static void Write(string x = "") // For Convenience
    {
        Console.Write(x);
    }
    public static string Read(string x = "") // For Convenience
    {
        Console.Write("\n" + x);
        x = Console.ReadLine();
        return x != null ? x : "" ;
    }
}
class Journal
{
    public List<Entry> entries { get; set;}

    public Journal()
    {
        entries = new();
    }

    public static Journal LoadFromFile(string path)
    {
        string x = "";
        try
        {
            x = File.ReadAllText(path);
        }
        catch (FileNotFoundException)
        {
            Program.Write("That directory could not be found");
        }
        if (x.Length > 0)
        {
            return JsonSerializer.Deserialize<Journal>(x);
        }
        else
        {
            Program.Write("AN ERROR AS OCCURRED");
            return null;
        }
    }
    public void SaveToFile(string path)
    {
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

        string x = JsonSerializer.Serialize<Journal>(this, options);
        if (x.Length > 2)
        {
            File.WriteAllText(path, x);
            if (File.ReadAllText(path).Length == x.Length)
            {
            Program.Write("-----Success-----");
            }
        }
          
    }
    public void AddNewEntry(bool fromFile = true)
    {
        entries.Add(new Entry(fromFile));
    }
    public void DeleteEntry(int i = -1)
    {
        if (i > -1 && i < entries.Count())
        {
            entries.RemoveAt(i);
        }
        else
        {
            Program.Write("Failed - Invalid number");
        }
    }
    public void EditEntry(int i)
    {
        if (i > -1 && i < entries.Count())
        {
            entries[i].Edit();
        }
        else
        {
            Program.Write("Failed - Invalid number");
        }
    }
}
class Entry
{
    public string date { get; set; }
    public Prompt prompt { get; set; }
    public string entry { get; set; }
    public Entry(){}
    public Entry(bool fromFile = true)
    {
        if (!fromFile)
        {
            date = $"-----{DateTime.Now}-----";
            prompt = new(false);
            entry = Program.Read($"\n{date}\nPrompt: {prompt.p}\nEntry: ");
        }
    }
    public void Edit()
    {
        Program.Write($"\n{DateTime.Now}\n{prompt.p}");
        entry += $"\n--{DateTime.Now}--\n";
        entry += Program.Read($"\n---{DateTime.Now}---\n");
    }
    public void Set(string date, string prompt)
    {

    }
    public void Display()
    {
        Program.Write($"\n{date}\n{prompt.p}\n{entry}\n");
    }
}
class Prompt
{
    public string p {get; set;}

    public Prompt(){}
    public Prompt(bool fromFile = true)
    {
        GetRandom();
    }
    public string GetRandom()
    {
        p = new List<string>(){"How are you today?","What did you do today?","Did you have a good day?","Where did you go today?","What did you see today?","What happened today?"}[new Random().Next(0,5)];
        return "";
    }
    public string Set(string s = "")
    {
        p = s;
        return p;
    }
}