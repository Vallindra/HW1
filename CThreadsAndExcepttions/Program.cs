namespace CThreadsAndExcepttions
{
    internal class Program
    {
        static void FaultyWorker()
        {
            Console.WriteLine("Thread starts.");
            int a = 0;
            int b = 10 / a;
            Console.WriteLine("Thread ends.");
        }
        static void Main(string[] args)
        {
            Thread t = new Thread(FaultyWorker);
            try
            {
                t.Start();
            }
            catch
            {
                Console.WriteLine("My thread faulted.");
            }

            Console.WriteLine("Press ENTER to quit.");
            Console.ReadLine();
        }
    }
}