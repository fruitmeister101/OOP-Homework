using System;

static class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello FinalProject World!");

        GM.NewGame(1);
        for (int i = 0; i < 20; i++)
        {
            Thread.Sleep(1000);
            GM.NewGame((int)GM.RNG.Next());
        }
    }

    public static void Print(string text="")
    {
        Console.Write(text);
    }
    public static string Read(string text=": ")
    {
        return "";
    }

}