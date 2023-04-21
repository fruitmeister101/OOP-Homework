using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep1 World!");
        AskName();
    }


    static void AskName(){
        string[] name = new string[2];
        
        Console.Write("What is Your First Name? : ");
        name[0] = Console.ReadLine();

        Console.Write("What is your Last Name? : ");
        name[1] = Console.ReadLine();

        char[] temp2 = name[0].ToCharArray();
        char[] temp3 = name[1].ToCharArray();

        char temp1 = temp2[0]; // Save the first letter of one

        temp2[0] = temp3[0]; // Swap one for the other
        //temp3[0] = temp1; // Swap the other for the saved
        temp3[0] = "N".ToCharArray()[0]; // Swap the other for the saved


        Console.Write($"\n{name[1]}'s the name. {name[0]} name.\nPleasure to... wait?\n{name[1]} Name's the {name[0]}.\nAre you alright?\n{new string(temp2)} {new string(temp3)}'s having a Stronk. Call a {name[1]}ulance.");
    }
}