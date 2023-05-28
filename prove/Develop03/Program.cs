using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Develop03 World!");
        
        
        string temp = MS.Read("Type in the Phrase you wish to be Tested on! =D");
        
        Phrase x = new Phrase(temp);
        MS.Test(x);
        
    }


}