using System;
class Program
{
    //public static int max = 50;
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Sandbox World!");
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
        Console.WriteLine($"Hello hello? Testing testing. You have looped {i} times.");
        
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
            if (i != -1)
            {
                _jobs.RemoveAt(i);
            }
        }
        public void PrintResume(){
            print($"Name: {_name}\nJobs:");
            for (int i = 0; i < _jobs.Count; i++)
            {
                string job = _jobs[i].DisplayJob
    ();
                print($"\n{i}: {job}");
            }
        }

        public void Edit(){
            int i = -1;
            do{

            
            print($"\nWhat would you like to do? (input number to select)\n1: Add Job\n2: Remove Job\n3: exit");
            try
            {
                i = Int32.Parse(read());
                
            }
            catch (System.InvalidOperationException)
            {
                print("Invalid selection\n\n");
                i = -1;
            }

            switch (i)
            {   
                case 1:
                    print("Enter Job Title : ");
                    AddJob(read());
                break;
                case 2:
                    print("Remove which job? : ");
                    for (int j = 0; j < _jobs.Count; i++)
                    {
                        string job = _jobs[j].DisplayJob
            ();
                        print($"\n{j}: {job}");
                    }
                    print("\n");
                    RemoveJob(Int32.Parse(read()));
                break;
                case 3:
                
                break;
                default:
                    print("Invalid Input");
                break;
            }





            }while(i == -1 || i == 1 || i == 2);
        }

    }
}