using System;

class Program
{
    //public static int max = 50;
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Sandbox World!");
        TestMethod(0);
    }


    static void TestMethod(int i)
    {
        int max = 50;
        Console.WriteLine($"Hello hello? Testing testing. You have looped {i} times.");
        
        for (int j = 0; j < max; j++)
        {
            Console.WriteLine($"Hello hello? Testing testing. You have looped the second {i} times.");
            
        }
        
        if(i < max){
            TestMethod(i + 1);
        }
    }
}