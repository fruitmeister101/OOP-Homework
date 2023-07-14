using System;

static class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello FinalProject World!");

        GM.NewGame(DateTime.Now.Microsecond);
    }

    public static void Print(string text="")
    {
        Console.Write(text);
    }
    public static char Read(string text="")
    {
        return Console.ReadKey(true).KeyChar;
    }

}