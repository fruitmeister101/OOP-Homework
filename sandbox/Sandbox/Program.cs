using System;
class Program
{
    
    //public static int max = 50;
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Insert Sandbox World!");
        //TestMethod(0);

        var x = new Resume();
        x.Edit();

    }

    public static void print(string str = ""){
        Console.Write(str);
    }
    public static void Print(string str = ""){
        Console.Write(str);
    }
    public static string read(){
        return Console.ReadLine();
    }
    public static string Read(){
        return Console.ReadLine();
    }
    static void TestMethod(int i)
    {
        int max = 50;
        print($"Hello hello? Testing testing. You have looped {i} times.");
        
        for (int j = 0; j < max; j++)
        {
            Console.WriteLine($"Hello hello? Testing testing. You have looped the second {i} times.");
            
        }
        
        if(i < max){
            TestMethod(i + 1);
        }
    }

    class Job
    {
        string _jobTitle;
        public Job(string jobTitle = ""){
            _jobTitle = jobTitle;
        }
        public string DisplayJob(){
            return $"{_jobTitle}";
        }
    }
    class Job1
    {
        string _jobTitle;
        public Job1(string jobTitle = ""){
            _jobTitle = jobTitle;
        }
        public string DisplayJob(){
            return $"{_jobTitle}";
        }
    }
    class Resume
    {
        string _name;
        public List<Job> _jobs;
        public Resume(){
            print("What is your Name?: ");
            _name = read();
            _jobs = new();
        }
        void AddJob(string job){
            _jobs.Add(new Job(job));
        }
        void RemoveJob(int i = -1){
            if (i < _jobs.Count && i > -1)
            {
                _jobs.RemoveAt(i);
            }
        }
        public void PrintResume(){
            print($"Name: {_name}\nJobs:");
            for (int i = 0; i < _jobs.Count; i++)
            {
                string job = _jobs[i].DisplayJob();
                print($"\n{i + 1}: {job}");
            }
        }

        public void Edit(){
            int i = -1;
            int x = 0;
            int[] y =  new int[] {-1, 1, 2, 3}; 
            do{

            
            print($"\n\nWhat would you like to do? (input number to select)\n1: Add Job\n2: Remove Job\n3: Print\n4: Exit\n5: Exit and Print\n: ");
            try
            {
                i = Int32.Parse(read());
                
            }
            catch (System.FormatException)
            {
                print($"Invalid Input {20 - x}");
                i = -1;
                x++;
            }

            switch (i)
            {   
                case -1: break;
                case 1:
                    print("Enter Job Title : ");
                    AddJob(read());
                break;
                case 2:
                    print("Remove which job? : ");
                    for (int j = 0; j < _jobs.Count; j++)
                    {
                        string job = _jobs[j].DisplayJob();
                        print($"\n{j + 1}: {job}");
                    }
                    print("\n");
                    try
                    {
                        RemoveJob(Int32.Parse(read()) - 1);
                    }
                    catch (System.FormatException)
                    {
                        print($"Invalid Input {20 - x}");
                        x++;
                    }
                break;
                case 3:
                    PrintResume();
                break;
                case 4:
                    Print("Thank you for your time\n\n");
                break;
                case 5:
                    Print("Thank you for your time\n\n");
                    PrintResume();
                break;
                default:
                    print($"Invalid Input {20 - x}");
                    i = -1;
                    x++;
                break;
            }


            if (x > 20)
            {
                Print("\n\nYou are not taking this seriously...\nTry again later\n\n");
                break;
            }

            Print("\n");
            }while(y.Contains(i));
        }

    }
}